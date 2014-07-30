using System.Collections.Generic;
using System.Data;
using Moq;

namespace ProEvoCanary.Tests
{
    public static class DataReaderTest
    {
        public static IDataReader Reader(Dictionary<string, object> dictionary)
        {
            var moq = new Mock<IDataReader>();
            var count = -1;

            moq.Setup(x => x.Read()).Returns(() => count < 2).Callback(() => count++);

            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
            {
                KeyValuePair<string, object> keyValuePairItem = keyValuePair;
                moq.Setup(x => x[keyValuePairItem.Key]).Returns(() => keyValuePairItem.Value);
            }
            return moq.Object;
        }
    }
}