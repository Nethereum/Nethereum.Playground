﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace Nethereum.Metamask.Blazor
{

    public class MetamaskBlazorInterop : IMetamaskInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public MetamaskBlazorInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async ValueTask<string> EnableEthereumAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.EnableEthereum");
        }

        public async ValueTask<bool> CheckMetamaskAvailability()
        {
            return await _jsRuntime.InvokeAsync<bool>("NethereumMetamaskInterop.IsMetamaskAvailable");
        }

        public async ValueTask<RpcResponseMessage> SendAsync(RpcRequestMessage rpcRequestMessage)
        {
            var response = await _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.Request", JsonConvert.SerializeObject(rpcRequestMessage));
            return JsonConvert.DeserializeObject<RpcResponseMessage>(response);
        }

        public async ValueTask<RpcResponseMessage> SendTransactionAsync(MetamaskRpcRequestMessage rpcRequestMessage)
        {
            var response = await _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.Request", JsonConvert.SerializeObject(rpcRequestMessage));
            return JsonConvert.DeserializeObject<RpcResponseMessage>(response);
        }

        public async ValueTask<string> SignAsync(string utf8Hex)
        {
            var rpcJsonResponse = await  _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.Sign", utf8Hex);
            return ConvertResponse<string>(rpcJsonResponse);
        }

        public async ValueTask<string> GetSelectedAddress()
        {
            var rpcJsonResponse = await _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.GetAddresses");
            var accounts = ConvertResponse<string[]>(rpcJsonResponse);
            if (accounts.Length > 0)
                return accounts[0];
            else
                return null;

        }


        [JSInvokable()]
        public static async Task MetamaskAvailableChanged(bool available)
        {
            await MetamaskHostProvider.Current.ChangeMetamaskAvailableAsync(available);
        }

        [JSInvokable()]
        public static async Task SelectedAccountChanged(string selectedAccount)
        {
            await MetamaskHostProvider.Current.ChangeSelectedAccountAsync(selectedAccount);
        }

        [JSInvokable()]
        public static async Task SelectedNetworkChanged(int chainId)
        {
            await MetamaskHostProvider.Current.ChangeSelectedNetworkAsync(chainId);
        }

        private T ConvertResponse<T>(string jsonResponseMessage)
        {
            var responseMessage = JsonConvert.DeserializeObject<RpcResponseMessage>(jsonResponseMessage);
            return ConvertResponse<T>(responseMessage);
        }

        private void HandleRpcError(RpcResponseMessage response)
        {
            if (response.HasError)
                throw new RpcResponseException(new JsonRpc.Client.RpcError(response.Error.Code, response.Error.Message,
                    response.Error.Data));
        }

        private T ConvertResponse<T>(RpcResponseMessage response,
            string route = null)
        {
            HandleRpcError(response);
            try
            {
                return response.GetResult<T>();
            }
            catch (FormatException formatException)
            {
                throw new RpcResponseFormatException("Invalid format found in RPC response", formatException);
            }
        }
    }
}