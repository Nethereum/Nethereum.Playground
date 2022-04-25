using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using NetDapps.Assemblies;
using Newtonsoft.Json;

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

        public bool Loading { get; set; }
        public bool ShowingComponentInfo { get; set; } = false;
        public string ComponentInfo { get; set; }
        public void ShowHideComponentInfo()
        {
            if (ShowingComponentInfo)
            {
                ShowingComponentInfo = false;
            }
            else
            {
                try
                {
                    ComponentInfo = JsonConvert.SerializeObject(UIComponentsInfo[SelectedUIComponentInfo], Formatting.Indented );
                }
                catch(Exception ex)
                {
                    ComponentInfo = "Error: " + ex.Message + ex.StackTrace;
                }

                ShowingComponentInfo = true;

            }
            StateHasChanged();
        }
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
            return assembly.UIComponents.First().Name;
        }

        public void UIComponentChanged(ChangeEventArgs evt)
        {
            SelectedUIComponentInfo = int.Parse(evt.Value.ToString());
            LoadSelected();
        }

        public void LoadSelected()
        {
            Loading = Compiler.IsInitialised();
            Compiler.WhenReady(LoadSelectedInternal);
        }

        public async Task LoadSelectedInternal()
        {
            Loading = true;
            StateHasChanged();
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
                    builder.OpenComponent(0, assembly.GetType(selected.UIComponents.First().EntryPoint));
                    Console.WriteLine("Component selected");
                    builder.CloseComponent();
                };
               
            }

            LoadedComponents[SelectedUIComponentInfo] = CurrentComponent;

            LoadedTitle = selected.UIComponents.First().Name;
            Loading = false;
            StateHasChanged();
        }
    }
}
