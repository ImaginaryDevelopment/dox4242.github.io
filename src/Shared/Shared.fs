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

type DecodedBuilding = {
    id:int
    q:int // quantity
    t:int // total
    m:int // max
    r:int // total all-time
    e:int // max all-time
}
type EventResource = {
    res: int
}
type FactionCoinRec = {
    factionCoins:decimal
    royalExchanges:int
}
type Lineage = {
    lev: int
}
type MaelstromTarget = {
    targeteffect:int
}
type SaveOptions = {
    block_bg_clicks: int
    buy_all_exchanges: int
    buy_all_upgrades: int
    disable_autoclicks: int
    disable_buymax_button: int
    disable_click_particles: int
    disable_click_text: int
    disable_cloud_check: int
    disable_exchange_warning: int
    disable_gift_of_gods: int
    disable_gift_of_heroes: int
    disable_gift_of_kings: int
    disable_multibuy_series: int
    disable_ruby_warning: int
    disable_sliding_menu: int
    disable_trophy_groups: int
    disable_trophy_stacks: int
    disable_tutorials: int
    disable_upgrade_groups: int
    disable_upgrade_stacks: int
    enable_extended_lists: int
    event_mana_color: int
    event_particle_icon: int
    hide_purchased_upgrades: int
    hide_unavail_research: int
    notation: int
    sort_purchased_upgrades: int
    sort_unpurchased_upgrades: int
    spell_effect_icons: int
    spell_tooltips_persist: int
    thousands_sep: int
}
type SaveSpell = {
    a:bool
    active0:int
    active1: int
    active2: decimal
    activeTiers: int
    c: int
    e: decimal
    id: int
    n: int
    n2: int
    n3: int
    r: int
    s: int
    t: int
    tierstat1: int
}
type SaveStat = {
    stats: decimal
    statsRei: decimal
    statsReset: decimal
}
type SaveTrophy = {
    id:int
    u1:bool
    u2:int
}
type SaveUpgrade = {
    id: int
    s: int
    u1: bool
    u2: bool
    u3: bool
}

type DecodedSave = {
    alignment: int
    artifactRngState: int
    artifactSet: int
    ascension: int
    bFaction: int
    breathEffects: int
    buildings: DecodedBuilding[] // not really an array, object with numberic props
    buyMode: int
    cTimer: int
    catalystTargets: obj[]
    chaosMadnessTarget: int
    chargedTimer: int
    coins: decimal
    combinationBL: int
    comboStrikeCont: int
    consecutiveDays: int
    contingency: bool
    contingencyValue: int
    ctaFactionCasts: int
    currentDay: int
    djinnchallenge4_1: int
    djinnchallenge4_2: int
    dreamcatcher1: int
    dreamcatcher2: int
    eggRngState: int
    eggStackSize: int
    elitePrestigeFaction: int
    eventResources: EventResource[]
    excavBuyMode: int
    excavations: int
    faceunion1: int
    faceunion2: int
    faction: int
    factionCoins: FactionCoinRec[]
    frozenLightningTimer: int
    gems: bigint
    goblinTimer: int
    halloweenMonsters: int
    hourGlassTimer: int
    kcTimer: int
    lastGiftDate: int
    lastsave: int
    limitedWishStrength: decimal
    limitedWishTarget: int
    lineageFaction: int
    lineageLevels: Lineage[]
    mTimer: int
    maelstromTargets: MaelstromTarget[]
    mana: decimal
    mercExtraUpgrade: int
    mercSpell1: int
    mercSpell2: int
    mercUnion: int
    mercUnion2: int
    miracleTier: int
    miracleTimer: int
    newField32: int
    oTimer: int
    oTimer2: int
    oTimer3: int
    options: SaveOptions
    other21: int
    playfabSeason: int
    prestigeFaction: int
    reBuyMode: int
    reincarnation: int
    rubies: uint
    sTimer: int
    saveRevision: int
    saveVersion: int
    season: int
    seasonN: int
    secondaryAlignment: int
    snowballScryUses: int
    snowballSize: int
    spells: SaveSpell[] // not an array
    stats: SaveStat[]
    stoneheartSet: int
    strikeTier: int
    trophies: SaveTrophy[]
    tutorials: obj
    upgrades: SaveUpgrade[]
    wealthStormTimer: int
}

