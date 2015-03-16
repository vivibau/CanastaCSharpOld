#include "Game.h"

Game::Game(GameType_e gameType, std::string gameName, int numberOfPlayers, int numberOfTeams)
{
    m_gameType = gameType;
    m_gameName = gameName;
    m_numberOfPlayers = numberOfPlayers;
    m_numberOfTeams = numberOfTeams;
    m_players.clear();
    m_currentPlayer = -1;
    m_gameState = WaitingForPlayers_e;
}

std::string Game::getGameName()
{
    return m_gameName;
}

ResponseCode_e Game::addPlayer(std::string playerName)
{
    int numberOfPlayers = m_players.size();
    if (numberOfPlayers == m_numberOfPlayers)
        return GameFull_e;

    for (int i = 0; i < numberOfPlayers; i++)
    {
        if (m_players[i]->getName() == playerName)
            return PlayerExists_e;
    }

    m_players.push_back(new Player(playerName));
    return OK_e;
}

std::vector<Player*>& Game::getPlayers()
{
    return m_players;
}

void Game::update(std::string playerName, Operation_e operation, std::string data)
{
    m_history.push_back(new Update(playerName, operation, data));
    for (unsigned int i = 0; i < m_players.size(); i++)
        if (m_players[i]->getIndexHistory() == -1)
            m_players[i]->setIndexHistory(m_history.size());
}

int Game::getIndexHistory()
{
    return m_history.size();
}

GameState_e Game::getGameState()
{
    return m_gameState;
}

void Game::setGameState(GameState_e state)
{
    m_gameState = state;
}
