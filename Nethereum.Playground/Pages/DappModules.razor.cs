﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using NetDapps.Assemblies;

namespace Nethereum.Playground.Pages
{
    public class DappModulesViewModel:ComponentBase
    {
        [Inject] public HttpClient Client { get; set; }

        [Inject] public Compiler Compiler { get; set; }
        public RenderFragment CurrentComponent { get; set; } = null;
        public Dictionary<int, RenderFragment> LoadedComponents { get; set; } = new Dictionary<int, RenderFragment>();

        public List<UIAssemblyLoadInfo> UIComponentsInfo { get; set; }
        public int SelectedUIComponentInfo { get; protected set; }

        public string LoadedTitle { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UIComponentsInfo = new List<UIAssemblyLoadInfo>();
            var loaded = await Client.GetJsonAsync<UIAssemblyLoadInfo[]>("uiAssemblies.json");
            UIComponentsInfo.AddRange(loaded);
            SelectedUIComponentInfo = 0;
            LoadSelected();
        }

        public string GetDisplayTitle(UIAssemblyLoadInfo assembly)
        {
            return assembly.UIComponents.First();
        }

        public void UIComponentChanged(ChangeEventArgs evt)
        {
            SelectedUIComponentInfo = int.Parse(evt.Value.ToString());
            LoadSelected();
        }

        public void LoadSelected()
        {
            Compiler.WhenReady(LoadSelectedInternal);
        }

        public async Task LoadSelectedInternal()
        {
            var selected = UIComponentsInfo[SelectedUIComponentInfo];
            await AssemblyCache.Current.LoadAssembly(Client, selected);

            if (LoadedComponents.ContainsKey(SelectedUIComponentInfo))
            {
                CurrentComponent = LoadedComponents[SelectedUIComponentInfo];
            }
            else
            {
                CurrentComponent = builder =>
                {
                    var assembly = AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(x => x.FullName.StartsWith(selected.FullName));

                    Console.WriteLine("Component selected");
                    builder.OpenComponent(0, assembly.GetType(selected.UIComponents.First()));
                    Console.WriteLine("Component selected");
                    builder.CloseComponent();
                };
            }

            LoadedComponents[SelectedUIComponentInfo] = CurrentComponent;

            LoadedTitle = selected.UIComponents.First();
            StateHasChanged();
        }
    }
}
