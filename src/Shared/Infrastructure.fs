module Infrastructure

let isValueString =
    function
    | null | "" -> false
    | x when System.String.IsNullOrWhiteSpace x -> false
    | _ -> true