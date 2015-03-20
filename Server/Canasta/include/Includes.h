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
    AskGameList_e   = 5,
    AskPlayers_e    = 6,
    ModifyGame_e    = 7,
    AddPlayer_e     = 8,
    Broadcast_e     = 9,
    AssignPlayer_e  = 10,
    LeaveGame_e     = 11,
    StartGame_e     = 12
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
    WaitingForPlayers_e = 1,
    InProgress_e        = 2,
    Ended_e             = 3
};

#endif // INCLUDES_H_INCLUDED
