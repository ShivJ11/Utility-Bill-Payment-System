using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CustomerPortal.Models;
using System.Configuration;
using System.Text;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerPortal.Common_code
{
    public class UserApiGateway
    {
        private IConfiguration config;
        string ApiURL;
        string EmailApiURL;
        string CardApiURL;
        string AccountApiURL;
        string BillApiURL;
        string TransactionApiURL;
        public UserApiGateway(IConfiguration _config)
        {
            config = _config;
            ApiURL = config.GetValue<string>("ApiPath:URL");
            EmailApiURL = config.GetValue<string>("EmailApiPath:URL");
            CardApiURL = config.GetValue<string>("CardApiPath:URL");
            AccountApiURL = config.GetValue<string>("AccountsApiPath:URL");
            BillApiURL= config.GetValue<string>("BillsApiPath:URL");
            TransactionApiURL=config.GetValue<string>("TransactionApiPath:URL");
        }

        public UserDetails GetUserFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var userDetails = new UserDetails();

                if (jwtToken != null)
                {
                    userDetails.status = int.Parse(jwtToken.Claims.First(x => x.Type == "status").Value);
                    userDetails.userID = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);
                    userDetails.uFirstName = jwtToken.Claims.First(x => x.Type == "username").Value;
                    userDetails.uEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
                    userDetails.uContactNumber = jwtToken.Claims.First(x => x.Type == "contact").Value;
                }
                return userDetails;
            }
            catch (Exception ex){
                return null;
            }
        }
        public async Task<string> LoginCheck(string email, string password)
        {
            TokenResponse tokenresponse = new TokenResponse();
            string UserDetails = config.GetValue<string>("ApiPath:UserDetails");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(ApiURL + UserDetails + "?Email=" + email + "&Password=" + password))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        //user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                        tokenresponse = JsonConvert.DeserializeObject<TokenResponse>(apiResponse);
                    }
                }
            }
            string token = tokenresponse.token;
            return token;
        }

        public async Task<List<CardDetails>> GetCardDetails(int userID)
        {
            List<CardDetails> card=new List<CardDetails>();
            string CardDetails = config.GetValue<string>("CardApiPath:Card");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(CardApiURL + CardDetails + "?id=" + userID))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        card=JsonConvert.DeserializeObject<List<CardDetails>>(apiResponse);
                    }
                }
            }
            return card;
        }
        public async Task<CardDetails> GetCardDetailsByCardID(int CardID)
        {
            CardDetails card = new CardDetails();
            string CardDetails = config.GetValue<string>("CardApiPath:CardById");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(CardApiURL + CardDetails + "?CardID=" + CardID))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        card = JsonConvert.DeserializeObject<CardDetails>(apiResponse);
                    }
                }
            }
            return card;
        }
        public async Task<List<TransactionDetails>> DisplayDetails(List<TransactionDetails> details)
        {
            List<TransactionDetails> trans = new List<TransactionDetails>();
            foreach (var transaction in details)
            {
                CardDetails card = new CardDetails();
                TransactionDetails td = new TransactionDetails();
                card = await GetCardDetailsByCardID(transaction.CardID);
                transaction.CardNumber = Convert.ToString(card.CardNumber);
                transaction.CardType = card.CardType;
                td.AccountNumber = transaction.AccountNumber;
                td.CardID = transaction.CardID;
                td.PaymentDate = transaction.PaymentDate;
                td.TransID = transaction.TransID;
                td.TransAmount = transaction.TransAmount;
                td.AccountID = transaction.AccountID;
                td.CardType = card.CardType;
                td.CardNumber = Convert.ToString(card.CardNumber);
                trans.Add(td);

            }
            return trans;
        }

        public async Task<List<TransactionDetails>> GetTransactionDetails(int userID)
        {
            List<TransactionDetails> details=new List<TransactionDetails>();
            string transactionDetails = config.GetValue<string>("TransactionApiPath:Transaction");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(TransactionApiURL + transactionDetails + "?id=" + userID))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        details=JsonConvert.DeserializeObject<List<TransactionDetails>>(apiResponse);
                    }
                }
            }
            return details;
        }

        public async Task<List<AccountDetails>> GetAccountDetails(int userID)
        {
            List<AccountDetails> accounts=new List<AccountDetails>();
            string AccountDetails = config.GetValue<string>("AccountsApiPath:Account");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(AccountApiURL + AccountDetails + "?id=" + userID))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        accounts=JsonConvert.DeserializeObject<List<AccountDetails>>(apiResponse);
                    }
                }
            }
            return accounts;
        }
        public async Task<Response> AddTransactionDetails(TransactionInput details)
        {
            Response res = new Response();
            string transactionDetails = config.GetValue<string>("TransactionApiPath:Transaction");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync<TransactionInput>(TransactionApiURL + transactionDetails, details))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<Response>(apiResponse);
                    }
                }
                return res;
            }

        }

        public async Task<Response> UpdateAccountStatus(int accID)
        {
            Response res = new Response();
            string AccountDetails = config.GetValue<string>("AccountsApiPath:UpdateAccount");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(AccountApiURL + AccountDetails + "?AccId=" + accID))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<Response>(apiResponse);
                    }
                }
                return res;
            }

        }

        public async Task<AccountResponse> AddAccountDetails(AccountDetails acc)
        {
            AccountResponse accResponse = new AccountResponse();
            string AccountDetails = config.GetValue<string>("AccountsApiPath:Account");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync<AccountDetails>(AccountApiURL + AccountDetails, acc))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        accResponse = JsonConvert.DeserializeObject<AccountResponse>(apiResponse);
                    }
                }
            }
            return accResponse;
        }

        public async Task<CardResponse> AddCardDetails(CardDetails card)
        {
            CardResponse cardResponse = new CardResponse();
            string CardDetails = config.GetValue<string>("CardApiPath:Card");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync<CardDetails>(CardApiURL+CardDetails,card))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        cardResponse = JsonConvert.DeserializeObject<CardResponse>(apiResponse);
                    }
                }
            }
            return cardResponse;
        }
        public async Task<RegisterResponse> RegisterUser(UserDetails user)
        {
            RegisterResponse userRegister = new RegisterResponse();
            string RegisterUser = config.GetValue<string>("ApiPath:RegisterUser");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync<UserDetails>(ApiURL + RegisterUser,user ))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        userRegister = JsonConvert.DeserializeObject<RegisterResponse>(apiResponse);
                    }

                }
                /*client.BaseAddress = new Uri(ApiPath.ApiURL);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<UserDetails>("", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }*/
            }

            return userRegister;
        }

        public async Task<bool> RegisterUserWelcomeEmail(EmailRequestModel email)
        {
            string RegisterEmail = config.GetValue<string>("EmailApiPath:RegisterEmail");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync<EmailRequestModel>(EmailApiURL + RegisterEmail, email))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        public async Task<bool> UserFeedbackEmail(ContactUsDetails details)
        {
			string feedbackEmail = config.GetValue<string>("EmailApiPath:FeedbackEmail");
			using (var client = new HttpClient())
			{
				using (var response = await client.PostAsJsonAsync<ContactUsDetails>(EmailApiURL + feedbackEmail, details))
				{
					if (response.IsSuccessStatusCode)
					{
						return true;
					}

				}
			}
			return false;
		}
    }
}
