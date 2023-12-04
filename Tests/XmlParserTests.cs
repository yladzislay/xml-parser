using XmlParser.Structures;
using XmlParser.Structures.CombinedStatus;
using Xunit;

namespace Tests
{
    public class XmlParserTests
    {
        [Fact]
        public void XmlParserCommonTest()
        {
            var xmlFilePath = Path.Combine("Resources", "status_1.xml");
            var xml = File.ReadAllText(xmlFilePath);
            
            var instrumentStatus = XmlParser.Parser.ParseInstrumentStatus(xml);

            Assert.NotNull(instrumentStatus);
            Assert.IsType<InstrumentStatus>(instrumentStatus);

            Assert.NotNull(instrumentStatus.DeviceStatusList);
            Assert.NotEmpty(instrumentStatus.DeviceStatusList);

            var firstDeviceStatus = instrumentStatus.DeviceStatusList[0];
            Assert.NotNull(firstDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedSamplerStatus>(firstDeviceStatus.RapidControlStatus.CombinedStatus);

            var secondDeviceStatus = instrumentStatus.DeviceStatusList[1];
            Assert.NotNull(secondDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedPumpStatus>(secondDeviceStatus.RapidControlStatus.CombinedStatus);

            var thirdDeviceStatus = instrumentStatus.DeviceStatusList[2];
            Assert.NotNull(thirdDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedOvenStatus>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
        }
    }
}