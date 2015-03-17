#ifndef PARSER_H
#define PARSER_H

#include <stdio.h>
#include <iostream>
#include "Includes.h"
#include <vector>
#include "Game.h"

class Parser
{
    public:
                        Parser(const char* input, ssize_t length);
                        ~Parser();
        void            parseData();
        void            updateGame(std::vector<Game*>& games);
        std::string     getResponse();
    protected:
    private:
        GameType_e      m_gameType;
        std::string     m_gameName;
        Operation_e     m_operation;

        std::string     m_data;
        std::string     m_playerName;
        std::string     m_selectedPlayer;

        int             m_numberOfPlayers;
        int             m_numberOfTeams;
        int             m_selectedTeam;

        std::string     m_message;
        std::string     m_response;

        void            parseCreateGame();
        void            parseBroadcast();
        void            parseAssignPlayer();
        void            updateGameAskStatus(std::vector<Game*>& games);
        void            updateGameCreateGame(std::vector<Game*>& games);
        void            updateGameJoinGame(std::vector<Game*>& games);
        void            updateGameAskGameList(std::vector<Game*>& games);
        void            updateGameAskPlayers(std::vector<Game*>& games);
        void            updateGameAddPlayer(std::vector<Game*>& games);
        void            updateGameBroadcast(std::vector<Game*>& games);
        void            updateGameAssignPlayer(std::vector<Game*>& games);
        void            updateGameRemovePlayer(std::vector<Game*>& games);

        Game*           getSelectedGame(std::vector<Game*>& games);
};

#endif // PARSER_H
