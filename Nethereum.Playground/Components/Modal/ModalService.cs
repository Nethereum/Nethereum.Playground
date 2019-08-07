using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

//Full credits to Chris Sainty: https://github.com/chrissainty/BlazorModal
//MIT License

//Copyright(c) 2019 Chris Sainty

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace Nethereum.Playground.Components.Modal
{
    public class ModalService
    {
        public event Action<string, RenderFragment> OnShow;
        public event Action OnClose;
      

        public void ShowModal<TComponent, TComponentModel>(string title, TComponentModel componentModel = null, string componentModelAttribute = null) where TComponent : ComponentBase where TComponentModel : class
        {
           var renderFragment = new RenderFragment(builder => { 
                builder.OpenComponent<TComponent>(0);
                if (componentModel != null)
                {
                    builder.AddAttribute(1, componentModelAttribute, componentModel);
                }

                builder.CloseComponent();
            });
            OnShow?.Invoke(title, renderFragment);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }

        
    }
}
