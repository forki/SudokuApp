module internal MainScene

open System
open Fable.Core
open Fable.Import
open Fable.Import.ReactNative
open Fable.Helpers.ReactNative
open Fable.Helpers.ReactNative.Props
open SudokuSolver

type MainSceneProperties = {
    Navigator:Navigator
}

type MainSceneState = {
    Sudoku:Sudoku
}

type MainScene (props) as this =
    inherit React.Component<MainSceneProperties,MainSceneState>(props)

    do this.state <- { 
        Sudoku =
            [[0; 0; 8;  3; 0; 0;  6; 0; 0]
             [0; 0; 4;  0; 0; 0;  0; 1; 0]
             [6; 7; 0;  0; 8; 0;  0; 0; 0]

             [0; 1; 6;  4; 3; 0;  0; 0; 0]
             [0; 0; 0;  7; 9; 0;  0; 2; 0]
             [0; 9; 0;  0; 0; 0;  4; 0; 1]

             [0; 0; 0;  9; 1; 0;  0; 0; 5]
             [0; 0; 3;  0; 5; 0;  0; 0; 2]
             [0; 5; 0;  0; 0; 0;  0; 7; 4]]
              |> toSudoku }


    member x.render () =
        let inputs =
          view 
            [ViewProperties.Style [
              ViewStyle.FlexDirection FlexDirection.Column
              ViewStyle.AlignSelf Alignment.Center ]
            ]
            [for i in 0..x.state.Sudoku.Length-1 ->
              view [ViewProperties.Style [ViewStyle.FlexDirection FlexDirection.Row]]
                [for j in 0..x.state.Sudoku.Length-1 ->   
                  textInput 
                    [TextInputProperties.MaxLength 1.
                     TextInputProperties.KeyboardType KeyboardType.Numeric
                     TextInputProperties.Style [
                       TextStyle.Width 20.
                       TextStyle.Height 40.
                       ]
                     TextInputProperties.OnChangeText 
                          (fun text ->
                              let sudoku = x.state.Sudoku
                              sudoku.[i].[j] <- int text
                              x.setState { x.state with Sudoku = sudoku })                       
                    ] 
                    (match x.state.Sudoku.[i].[j] with
                     | 0 -> ""
                     | v -> v.ToString()) ]]

        view [ Styles.sceneBackground ] 
          [ text [ Styles.titleText ] "Sudoku Solver"
            inputs
            Styles.button "Solve" (fun () ->
              x.setState { Sudoku = getFirstSolution x.state.Sudoku }   
            ) ]