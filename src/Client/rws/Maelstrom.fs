module Rws.Maelstrom

open Fable.Core
open Fable.Core.JS
open Fable.React
open Fable.React.Props

type Msg =
    | InputChanged of string
    | EnterSave
    | CopySave
    | ClearSave
let init () = // : Model * Cmd<Msg> =
    ()

let copy text = // not using dispatch/command. might be bad idea
        try
            Browser.Navigator.navigator.clipboard
            |> function
                | Some clipboard -> clipboard.writeText text |> ignore<Promise<unit>>
                | None -> eprintfn "No clipboard found"
        with _ -> ()

let saveInput (saveText:string) dispatch =
    let inputId = "saveInput"
    div[Class "panel-body input-group panelSaveInput"][
        label[Id "saveInputLabel";Class "input-group-addon"; HtmlFor "saveInput"][
            a [ Data("toggle","popover");Data("trigger","hover");Data("placement","bottom")
                Data("content","'Export your save from Realm Grinder and paste it in this field to view a forecast of eggs that you will find in the Easter event.'>(?)" )
            ][
                unbox "Save (?)"
            ]
        ]
        input [Id inputId; Class "form-control"; Type "text"; Name "saveInput"; Value saveText;OnChange (fun e -> e.Value |> InputChanged |> dispatch)]
        div [Class "input-group-btn"][
            let btn id (title:string) cls (clickMsg:Msg) =
                button [Id id; Class <| "btn btn-" + cls; Type "Button";OnClick (fun _ -> dispatch clickMsg)][unbox title]
            yield btn "doReEnter" "Re-Enter save" "success" EnterSave
            yield button [  Id "doSaveCopy";Class <| "btn btn-info"; Type "Button"
                            OnClick (fun _ -> copy saveText)
                        ][unbox "Copy save"]
            // yield btn "doSaveCopy" "Copy save" "info" CopySave
            yield btn "doSaveClear" "Clear save" "danger" ClearSave
        ]
    ]

let view () =
    div [Id "MS"; Class "tabcontent"][
        br []
        div [Id "maelstromMessage"][]
        br []
        div [Id "maelstromForecast"][]
    ]