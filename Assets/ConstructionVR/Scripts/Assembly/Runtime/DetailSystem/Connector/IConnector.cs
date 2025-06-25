using ConstructionVR.Assembly.GraphSystem;

namespace ConstructionVR.Assembly
{
    public interface IConnector
    {
        /// <summary>
        /// Хранит в себе тип самого конектера
        /// </summary>
        public ConnectorTypeNode ConnectorType { get; }
        /// <summary>
        /// Хранит в себе деталь, которая была подключена к данному конектеру
        /// </summary>
        public ConnectableDetail ConnectedDetail { get; }
    }
}
