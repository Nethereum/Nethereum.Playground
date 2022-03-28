var BlazorEditors = [];
BlazorEditors = BlazorEditors;

var hasRegisteredCompletionProviders = false;

function getSuggestions(model, position, language) {
    let editorModel = {
        script: model.getValue(),
        language: language
    };
    return DotNet.invokeMethodAsync('Nethereum.Playground', 'GetCompletionItems', editorModel, model.getOffsetAt(position));

}


function getQuickInfo(model, position, language) {
    let editorModel = {
        script: model.getValue(),
        language: language
    };
    return DotNet.invokeMethodAsync('Nethereum.Playground', 'GetQuickInfo', editorModel, model.getOffsetAt(position));

}

function getSignatureHelp(model, position, language) {
    let editorModel = {
        script: model.getValue(),
        language: language
    };
    return DotNet.invokeMethodAsync('Nethereum.Playground', 'GetSignatureCollection', editorModel, model.getOffsetAt(position));
}



function registerCompletionProvider() {
    if (!hasRegisteredCompletionProviders) {
        monaco.languages.register({ id: 'csharp' });

        monaco.languages.registerCompletionItemProvider('csharp',
            {
                triggerCharacters: ['.'],
                provideCompletionItems: function(model, position) {
                    return getSuggestions(model, position, 'csharp').then(x => {
                        console.log(x);
                        return {
                            suggestions: x
                        }
                    });

                },
            });

        monaco.languages.registerHoverProvider('csharp',
            {
                provideHover: function(model, position) {
                    return getQuickInfo(model, position, 'csharp').then(function(res) {
                        return {
                            contents: res
                        }
                    });
                }
            });

        monaco.languages.registerSignatureHelpProvider('csharp',
            {
                signatureHelpTriggerCharacters: ['(',','],
                provideSignatureHelp: function(model, position) {
                    return getSignatureHelp(model, position, 'csharp').then(function (res) {
                        console.log(res);
                        return res;

                    });
                }
            });

        hasRegisteredCompletionProviders = true;
    }
}


window.BlazorBitsMonacoInterop = {
    EditorInitialize: (editorModel) => {
       
        console.debug(`Registering new editor ${editorModel.id}...`);
        if (BlazorEditors.find(e => e.id === editorModel.id)) {
            console.error(`Refused to register duplicate editor ${editorModel.id}`);
        } else {
            registerCompletionProvider();
            let thisEditor = monaco.editor.create(document.getElementById(editorModel.id), {
                value: editorModel.script,
                language: editorModel.language,
                automaticLayout: true,
                quickSuggestions: false
            });
            monaco.editor.setTheme('vs-dark');

            console.debug(`Registered new editor ${editorModel.id}`);
            BlazorEditors.push({ id: editorModel.id, editor: thisEditor });
            
        }

        return true;
    },
    EditorGet: (editorModel) => {
        console.debug(`Getting editor for ${editorModel.id}...`);
        let myEditor = BlazorEditors.find(e => e.id === editorModel.id);
        console.debug(`Found: ${myEditor}`);
        if (!myEditor) {
            throw `Could not find a editor with id: '${editorModel.id}'`;
        }

        // Update the model
        editorModel.script = myEditor.editor.getValue();
       
        return editorModel;
    },

    EditorSet: (editorModel) => {
        console.debug(`Setting editor for ${editorModel.id}...`);
        let myEditor = BlazorEditors.find(e => e.id === editorModel.id);
        console.debug(`Found: ${myEditor}`);
        if (!myEditor) {
            throw `Could not find a editor with id: '${editorModel.id}'`;
        }

        // Update the editor
        myEditor.editor.setValue(editorModel.script);
        console.debug(`Setting value to success.`);

        return editorModel;
    }
} 




