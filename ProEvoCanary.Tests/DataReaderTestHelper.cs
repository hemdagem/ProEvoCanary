﻿using System.Collections.Generic;
using System.Data;
using Moq;

namespace ProEvoCanary.Tests
{
    public static class DataReaderTestHelper
    {
        public static IDataReader Reader(Dictionary<string, object> dictionary)
        {
            var moq = new Mock<IDataReader>();
            var count = 0;

            moq.Setup(x => x.Read()).Returns(() => count < 1).Callback(() => count++);

            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
            {
                var keyValuePairItem = keyValuePair;
                moq.Setup(x => x[keyValuePairItem.Key]).Returns(() => keyValuePairItem.Value);
            }
            return moq.Object;
        }
    }
}