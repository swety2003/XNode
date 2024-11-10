﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XLib.Node;

namespace AXNode.SubSystem.NodeLibSystem.Define.Functions
{
    public class Func_Delay : NodeBase
    {
        #region 生命周期

        public override void Init()
        {
            SetViewProperty(NodeColorSet.Function, "Delay", "延迟执行");

            PinGroupList.Add(new ExecutePinGroup(this, "延迟指定毫秒后执行"));
            PinGroupList.Add(new DataPinGroup(this, "int", "时长", "5000") { Readable = false, Writeable = false });

            InitPinGroup();
        }

        #endregion

        #region NodeBase 方法

        protected override async void ExecuteNode()
        {
            await Task.Delay(int.Parse(GetData(1)));
            GetPinGroup<ExecutePinGroup>().Execute();
        }

        public override string GetTypeString() => nameof(Func_Delay);

        public override Dictionary<string, string> GetParaDict()
        {
            return new Dictionary<string, string>
            {
                { "Time", GetData(1) }
            };
        }

        public override void LoadParaDict(string version, Dictionary<string, string> paraDict)
        {
            try
            {
                SetData(1, paraDict["Time"]);
            }
            catch (Exception) { }
        }

        protected override NodeBase CloneNode() => new Func_Delay();

        #endregion
    }
}