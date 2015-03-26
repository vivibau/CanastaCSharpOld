#ifndef INCLUDES_H_INCLUDED
#define INCLUDES_H_INCLUDED

enum GameType_e
{
    NoType_e        = 0,
    Canasta_e       = 1
};

enum Operation_e
{
    NoOperation_e   = 0,
    CreateGame_e    = 1,
    DeleteGame_e    = 2,
    JoinGame_e      = 3,
    AskStatus_e     = 4,
    ModifyGame_e    = 7,
    Broadcast_e     = 9,
    AssignPlayer_e  = 10,
    LeaveGame_e     = 11,
    StartGame_e     = 12,
    GetBoard_e      = 13,
    AskGameList_e   = 14,
    AskPlayerList_e = 15
};

enum ResponseCode_e
{
    OK_e                = 0,
    GameExists_e        = 1,
    GameInexistent_e    = 2,
    GameNotOwned_e      = 3,
    GameFull_e          = 4,
    PlayerExists_e      = 6,
    PlayerInexistent_e  = 7,
    GameStarted_e       = 8
};

enum GameState_e
{
    StateUnknown_e      = 0,
    WaitingForPlayers_e = 1,
    InProgress_e        = 2,
    Ended_e             = 3
};


enum PieceState_e
{
    InStack_e           = 0,
    Rare_e              = 1,
    InPot_e             = 2,
    OnBoard_e           = 3,
    Displayed_e         = 4,
    Displayed2_e        = 5
};
/*
enum AskStatusOpType_e
{
    AskGameList_e       = 0,
    AskPlayerList_e     = 1
};
*/

#endif // INCLUDES_H_INCLUDED
