var BlazorEditors = [];
BlazorEditors = BlazorEditors;

window.BlazorBitsMonacoInterop = {
    EditorInitialize: (editorModel) => {
        console.debug(`Registering new editor ${editorModel.id}...`);
        let thisEditor = monaco.editor.create(document.getElementById(editorModel.id), {
            value: editorModel.script,
            language: editorModel.language,
            automaticLayout: true
        });

        monaco.editor.setTheme('vs-dark');

        if (BlazorEditors.find(e => e.id === editorModel.id)) {
            console.error(`Refused to register duplicate editor ${editorModel.id}`);
        }
        else {
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
        editorModel.Script = myEditor.editor.getValue();

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




