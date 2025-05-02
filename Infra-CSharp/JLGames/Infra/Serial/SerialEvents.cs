namespace JLGames.Infra.Serial
{
    public static class SerialEvents
    {
        /// <summary>
        /// Serial module start finish event
        /// </summary>
        public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";

        /// <summary>
        /// Serial module stop finish event
        /// </summary>
        public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";

        /// <summary>
        /// Serial manger start finish event
        /// </summary>
        public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";

        /// <summary>
        /// Serial manger stop finish event
        /// </summary>
        public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
    }
}