namespace Shared

type GoodBuilding =
    | WarriorBarracks
    | KnightsJoust
    | WizardTower
    | Cathedral
    | Citadel
    | RoyalCastle
    | HeavensGate
    with
        static member GetTier =
            function
            | WarriorBarracks -> 4
            | KnightsJoust -> 5
            | WizardTower -> 6
            | Cathedral -> 7
            | Citadel -> 8
            | RoyalCastle -> 9
            | HeavensGate -> 10
type AlignedBuilding =
    | Good of GoodBuilding
type Building =
    | Farm
    | Inn
    | Blacksmith
    | AlignedBuilding of AlignedBuilding
    | HallOfLegends
    with
        static member GetTier =
            function
            | Farm -> 1
            | Inn -> 2
            | Blacksmith -> 3
            | AlignedBuilding (Good x) -> GoodBuilding.GetTier x
            | HallOfLegends -> 11



