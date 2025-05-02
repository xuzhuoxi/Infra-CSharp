namespace JLGames.Infra.Service
{
    /// <summary>
    /// service event
    /// 服务事件
    /// </summary>
    public static class ServiceEvents
    {
        /// <summary>
        /// A single service initializes the progress update event, which is dispatched by the service instance.
        /// The service instance must be an implementation class of the IProgressingService interface
        /// 单个服务初始化进度更新事件，由服务实例调度。服务实例必须为IProgressingService接口的实现类
        /// </summary>
        public const string OnServiceProcessing = "ServiceEvents.OnServiceProcessing";

        /// <summary>
        /// Service initialization process progress update
        /// 服务初始化进程进度更新
        /// </summary>
        public const string OnInitializationProcessing = "ServiceEvents:OnInitializationProcessing";

        /// <summary>
        /// Service initialization process completed
        /// 服务初始化进程完成
        /// </summary>
        public const string OnInitializationFinish = "ServiceEvents:OnInitializationFinish";

        //----------

        /// <summary>
        /// Service argument injection result event, dispatched by ServiceManager.
        /// Succ=true when the service implements the IArgumentService interface.
        /// 服务参数注入结果事件, 由ServiceManager调度。当服务实现IArgumentService接口时，Succ=true。
        /// Event data format: ServiceResultEventData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceInjected = "ServiceEvents:OnServiceInjected";

        /// <summary>
        /// All service argument injection completion event
        /// 全部服务参数注入完成事件
        /// Event data format: null
        /// 事件数据格式：null
        /// </summary>
        public const string OnServiceAllInjected = "ServiceEvents:OnServiceAllInjected";

        /// <summary>
        /// Service activation result event, dispatched by ServiceManager.
        /// Succ=true when the service implements the IWakableService interface.
        /// 服务激活结果事件, 由ServiceManager调度。当服务实现IAwakableService接口时，Succ=true。
        /// Event data format: ServiceResultEventData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceAwaked = "ServiceEvents:OnServiceAwaked";

        /// <summary>
        /// All service activation completion event
        /// 全部服务激活完成事件
        /// Event data format: null
        /// 事件数据格式：null
        /// </summary>
        public const string OnServiceAllAwaked = "ServiceEvents:OnServiceAllAwaked";

        //----------

        /// <summary>
        /// Single service initialization start event, dispatched by ServiceManager
        /// 单个服务初始化开始事件，由ServiceManager调度
        /// Event data format: ServiceResultData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceInitStart = "ServiceEvents:OnServiceInitStart";

        /// <summary>
        /// A single service initialization complete event, which are dispatched by the service instance.
        /// Re-dispatched after being captured by ServiceManager.
        /// 单个服务初始化完成事件，由服务实例调度。被ServiceManager捕获后重新调度。
        /// Event data format: {named:string}
        /// 事件数据格式：{named:string}
        /// </summary>
        public const string OnServiceInited = "ServiceEvents:OnServiceInited";

        /// <summary>
        /// All service initialization complete event
        /// 全部服务初始化完成事件
        /// Event data format: len:int
        /// 事件数据格式：len:int
        /// </summary>
        public const string OnServiceAllInited = "ServiceEvents:OnServiceAllInited";

        /// <summary>
        /// Single service data initialization start event, dispatched by ServiceManager
        /// 单个服务数据初始化开始事件，由ServiceManager调度
        /// Event data format: ServiceResultData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceDataInitStart = "ServiceEvents:OnServiceDataInitStart";

        /// <summary>
        /// A single service data initialization complete event, which are dispatched by the service instance.
        /// Re-dispatched after being captured by ServiceManager.
        /// 单个服务数据初始化完成事件，由服务实例调度。被ServiceManager捕获后重新调度。
        /// Event data format: {named:string}
        /// 事件数据格式：{named:string}
        /// </summary>
        public const string OnServiceDataInited = "ServiceEvents:OnServiceDataInited";

        /// <summary>
        /// All service data initialization complete event
        /// 全部服务数据初始化完成事件
        /// Event data format: len:int
        /// 事件数据格式：len:int
        /// </summary>
        public const string OnServiceDataAllInited = "ServiceEvents:OnServiceDataAllInited";


        //----------

        /// <summary>
        /// A single  data service load data start event, dispatched by ServiceManager
        /// Succ=true when the service implements the ILoadDataService interface.
        /// 单个服务数据加载数据开始事件，由ServiceManager调度。当服务实现ILoadDataService接口时，Succ=true。
        /// Event data format: ServiceResultData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceDataLoadStart = "ServiceEvents:OnServiceDataLoadStart";

        /// <summary>
        /// A single data service load data completion event, which are dispatched by the service instance.
        /// Re-dispatched after being captured by ServiceManager.
        /// 单个数据服务加载数据完成事件，由服务实例调度。被ServiceManager捕获后重新调度。
        /// Event data format: {named:string}
        /// 事件数据格式：{named:string}
        /// </summary>
        public const string OnServiceDataLoaded = "ServiceEvents:OnServiceDataLoaded";

        /// <summary>
        /// All data service loading data completion event
        /// 全部数据服务加载数据完成事件
        /// Event data format: len:int
        /// 事件数据格式：len:int
        /// </summary>
        public const string OnServiceDataAllLoaded = "ServiceEvents:OnServiceDataAllLoaded";

        //----------

        /// <summary>
        /// A single data service save data start event, dispatched by ServiceManager
        /// Succ=true when the service implements the ISaveDataService interface.
        /// 单个服务数据保存数据开始事件，由ServiceManager调度。当服务实现ISaveDataService接口时，Succ=true。
        /// Event data format: ServiceResultData
        /// 事件数据格式：ServiceResultData
        /// </summary>
        public const string OnServiceDataSaveStart = "ServiceEvents:OnServiceDataSaveStart";

        /// <summary>
        /// A single data service saves data completion events, which are dispatched by the service instance.
        /// Re-dispatched after being captured by ServiceManager.
        /// 单个数据服务保存数据完成事件，由服务实例调度。被ServiceManager捕获后重新调度。
        /// Event data format: {named:string}
        /// 事件数据格式：{named:string}
        /// </summary>
        public const string OnServiceDataSaved = "ServiceEvents:OnServiceDataSaved";

        /// <summary>
        /// All data services save data complete event
        /// 全部数据服务保存数据完成事件
        /// Event data format: len:int
        /// 事件数据格式：len:int
        /// </summary>
        public const string OnServiceDataAllSaved = "ServiceEvents:OnServiceDataAllSaved";
    }
}