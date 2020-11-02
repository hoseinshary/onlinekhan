using NasleGhalam.Common;
using NasleGhalam.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.WindowsApp
{
    public class WebService : IDisposable
    {
        private readonly HttpClient _client;

        public WebService()
        {
            _client = new HttpClient { BaseAddress = new Uri(ConstantSettings.ApiUrl) };
        }

        #region ### User ###
        public async Task<ResponseObject<string>> Login(LoginViewModel login)
        {
            try
            {
                var strContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("User/Login", strContent);
                var result = await response.Content.ReadAsStringAsync();

                var resultObject = JsonConvert.DeserializeObject<ResponseObject<string>>(result);
                var resultObject1 = JsonConvert.DeserializeObject<LoginResultViewModel>(result);
                SetToken(resultObject.Data); // set token
                return resultObject;
            }
            catch (Exception e)
            {
                LogWriter.LogException(e);
                
            }

            return new ResponseObject<string>();
        }

        public void SetToken(string token)
        {
            if (_client.DefaultRequestHeaders.Contains("Token"))
            {
                _client.DefaultRequestHeaders.Remove("Token");
            }
            _client.DefaultRequestHeaders.Add("Token", token);
        }
        #endregion


       // #region ### FavoriteProduct ###
        //public async Task<ResponseObject<FavoriteProductViewModel>> FavoriteProductDetail(Guid id)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProduct/{id}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductViewModel>();
        //}

        //public async Task<List<FavoriteProductViewModel>> FavoriteProductList()
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync("FavoriteProduct/List");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductViewModel>> FavoriteProductCreate(string productLink, string productName, long price, long target)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            productName,
        //            productLink,
        //            price,
        //            target
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProduct/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductViewModel>> FavoriteProductUpdate(Guid id, long price, long target, bool disable)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            id,
        //            price,
        //            target,
        //            disable
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProduct/Update", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductViewModel>();
        //}
        //#endregion

        //#region ### FavoriteProductEvent ###
        //public async Task<ResponseObject<FavoriteProductEventViewModel>> FavoriteProductEventDetail(Guid id)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductEvent/{id}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductEventViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductEventViewModel>();
        //}

        //public async Task<List<FavoriteProductEventViewModel>> FavoriteProductEventList(Guid favoriteProductId)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductEvent/List/{favoriteProductId}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductEventViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductEventViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductEventViewModel>> FavoriteProductEventCreate(Guid favoriteProductId, string eventName, bool isDone, long price, long target)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            Event = eventName,
        //            isSuccessful = isDone,
        //            price,
        //            target,
        //            favoriteProductId
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProductEvent/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductEventViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductEventViewModel>();
        //}

        //public async Task<List<ResponseObject<FavoriteProductEventViewModel>>> FavoriteProductEventCreate(List<FavoriteProductViewModel> favoriteProducts, string eventName, bool isDone)
        //{
        //    var responseList = new List<ResponseObject<FavoriteProductEventViewModel>>();
        //    foreach (var favoriteProduct in favoriteProducts)
        //    {
        //        try
        //        {
        //            var result = await FavoriteProductEventCreate(favoriteProduct.Id, eventName, isDone,
        //                favoriteProduct.Price, favoriteProduct.Target);
        //            responseList.Add(result);
        //        }
        //        catch (Exception e)
        //        {
        //            LogWriter.LogException(e);
        //        }
        //    }

        //    return responseList;
        //}
        //#endregion

        //#region ### FavoriteProductPrice ###
        //public async Task<List<FavoriteProductPriceViewModel>> FavoriteProductPriceList(Guid favoriteProductId)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductPrice/List/{favoriteProductId}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductPriceViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductPriceViewModel>();
        //}
        //#endregion

        //#region ### UserSummary ###
        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryDetail(Guid id)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"UserSummary/{id}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}

        //public async Task<List<UserSummaryViewModel>> UserSummaryList()
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync("UserSummary/List");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<UserSummaryViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<UserSummaryViewModel>();
        //}

        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryCreate()
        //{
        //    try
        //    {
        //        var strContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("UserSummary/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}

        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryUpdate(Guid id, int countOfCrawl)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            id,
        //            countOfCrawl
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("UserSummary/Update", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}
        //#endregion

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
