#ifndef GAME_H
#define GAME_H

#include "Player.h"
#include <vector>
#include <iostream>
#include "Includes.h"
#include "Update.h"

class Game
{
    public:
                                Game(GameType_e gameType, std::string gameName, int numberOfPlayers, int numberOfTeams);
                                ~Game();
        ResponseCode_e          addPlayer(std::string playerName);
        ResponseCode_e          removePlayer(std::string playerName);
        std::string             getGameName();
        std::vector<Player*>&   getPlayers();
        void                    update(std::string playerName, Operation_e operation, std::string data);
        int                     getHistorySize();
        GameState_e             getGameState();
        void                    setGameState(GameState_e state);
        std::vector<Update*>&   getHistory();
        void                    generateGame();
        std::string             responsePlayersNameTeamOrder();
        std::string             responseFullGame();
        std::string             responseHistory(int index);
    protected:
        GameType_e              m_gameType;
        std::string             m_gameName;
        int                     m_numberOfPlayers;
        int                     m_numberOfTeams;
        GameState_e             m_gameState;
        std::vector<Player*>    m_players;
        int                     m_currentPlayer;
        std::vector<Update*>    m_history;
        std::vector<int>        m_stack;
        std::vector<int>        m_pot;
        int                     m_rare;
    private:
};

#endif // GAME_H
