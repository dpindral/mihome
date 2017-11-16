using System;
using MiHomeLibrary.Devices;
using NUnit.Framework;

namespace MiHomeLibrary.Test
{
    [TestFixture]
    public class TokenKeeperTest
    {
        [Test]
        public void GetTokenForGateway_EmptyList_Ok()
        {
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            Assert.That(tokenKeeper.GetTokenForGateway("aaa"), Is.Null);
        }
        
        [Test]
        public void GetTokenForGateway_SingleToken_Ok()
        {
            string gid = "aaa";
            string token = "123456";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            tokenKeeper.SetTokenForGateway(gid, token);
            Assert.That(tokenKeeper.GetTokenForGateway(gid), Is.EqualTo(token));
        }
        
        [Test]
        public void GetTokenForGateway_ManyTokens_Ok()
        {
            string gid = "aaa";
            string token = "123456";
            string gid2 = "bbb";
            string token2 = "7655432";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            tokenKeeper.SetTokenForGateway(gid, token);
            tokenKeeper.SetTokenForGateway(gid2,token2);
            Assert.That(tokenKeeper.GetTokenForGateway(gid), Is.EqualTo(token));
            Assert.That(tokenKeeper.GetTokenForGateway(gid2), Is.EqualTo(token2));
            
        }
        
        [Test]
        public void GetTokenForGateway_TokenUpdate_Ok()
        {
            string gid = "aaa";
            string token = "123456";
            string token2 = "7655432";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            tokenKeeper.SetTokenForGateway(gid, token);
            tokenKeeper.SetTokenForGateway(gid,token2);
            Assert.That(tokenKeeper.GetTokenForGateway(gid), Is.EqualTo(token2));

        }
        
        [Test]
        public void GetTokenForGateway_WrongGatewaySid_ArgumentException()
        {
            string gid = "";
            string token = "123456";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            Assert.Throws<ArgumentException>(() => tokenKeeper.SetTokenForGateway(gid, token));

        }
        
        [Test]
        public void GetTokenForGateway_GatewaySidIsNull_ArgumentException()
        {
            string token = "123456";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            Assert.Throws<ArgumentException>(() => tokenKeeper.SetTokenForGateway(null, token));

        }
        
        [Test]
        public void GetTokenForGateway_WrongToken_ArgumentException()
        {
            string gid = "aaa";
            string token = "";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            Assert.Throws<ArgumentException>(() => tokenKeeper.SetTokenForGateway(gid, token));

        }
        
        [Test]
        public void GetTokenForGateway_TokenIsNull_ArgumentException()
        {
            string gid = "aaa";
            
            TokenKeeperService tokenKeeper = new TokenKeeperService();
            Assert.Throws<ArgumentException>(() => tokenKeeper.SetTokenForGateway(gid, null));

        }
    }
}