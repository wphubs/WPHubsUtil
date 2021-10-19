using System;
using WPHubsUtil.Rand;
using Xunit;

namespace WPHubsUtilTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetFileRndName()
        {
            
               var x0 = RandomUtils.GetRandWord(10);;
            Assert.NotEmpty(x0);
        }
    }
}
