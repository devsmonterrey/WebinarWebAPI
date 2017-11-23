using System;
using System.Data.Entity;
using System.Linq;
using WebApiToken.Interfaces;
using WebApiToken.Models;

namespace WebApiToken
{
    public class TokenServices:ITokenServices
    {
        public Tokens GenerateToken(int userId)
        {
            webinarModel webinarModel = new webinarModel();
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddDays(5);
            var tokenDomain = new Tokens
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };
            webinarModel.Tokens.Add(tokenDomain);
            return tokenDomain;
        }

        public bool ValidateToken(string tokenId)
        {
            webinarModel webinarModel = new webinarModel();
            var token = webinarModel.Tokens.First(x => x.AuthToken == tokenId && x.ExpiresOn > DateTime.Now);
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = DateTime.Now.AddDays(5);
                webinarModel.Entry(token).State = EntityState.Modified;
                webinarModel.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Kill(string tokenId)
        {
            webinarModel webinarModel = new webinarModel();
            var tokens = webinarModel.Tokens.Where(x => x.AuthToken == tokenId);
            foreach (Tokens token in tokens)
            {
                webinarModel.Entry(token).State = EntityState.Deleted;
                webinarModel.SaveChanges();
            }
            if (webinarModel.Tokens.Where(x => x.AuthToken == tokenId).ToList().Count == 0) return true;
            return false;
        }

        public bool DeleteByUserId(int userId)
        {
            webinarModel webinarModel = new webinarModel();
            var tokens = webinarModel.Tokens.Where(x => x.UserId== userId);
            foreach (Tokens token in tokens)
            {
                webinarModel.Entry(token).State = EntityState.Deleted;
                webinarModel.SaveChanges();
            }
            if (webinarModel.Tokens.Where(x => x.UserId == userId).ToList().Count == 0) return true;
            return false;
        }
    }
}