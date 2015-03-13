#ifndef GAME_H
#define GAME_H

#include "Player.h"
#include <vector>
#include <iostream>
#include "Includes.h"

class Game
{
    public:
                                Game() {}
                                Game(GameType_e gameType, std::string gameName, int numberOfPlayers, int numberOfTeams);
        ResponseCode_e          addPlayer(std::string playerName);
        std::string             getGameName();
        std::vector<Player*>&   getPlayers();
    protected:
        GameType_e              m_gameType;
        std::string             m_gameName;
        std::vector<Player*>    m_players;
        int                     m_numberOfPlayers;
        int                     m_numberOfTeams;
        int                     m_currentPlayer;
        GameState_e             m_gameState;
    private:
};

#endif // GAME_H
