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

ResponseCode_e Game::removePlayer(std::string playerName)
{
    int pos = -1;
    for (unsigned int i = 0; i < m_players.size(); i++)
    {
        if (m_players[i]->getName() == playerName)
            pos = i;
    }

    if (pos == -1) return PlayerInexistent_e;
    Player* p = m_players[pos];
    int order = p->getOrder();
    int team = p->getTeam();

    m_players.erase(m_players.begin() + pos);
    delete(p);

    for (unsigned int i = 0; i < m_players.size(); i++)
        if (m_players[i]->getOrder() > order && m_players[i]->getTeam() == team)
            m_players[i]->setOrder(m_players[i]->getOrder() - m_numberOfTeams);
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
            m_players[i]->setIndexHistory(m_history.size() - 1);
}

int Game::getHistorySize()
{
    return m_history.size();
}

std::vector<Update*>& Game::getHistory()
{
    return m_history;
}

GameState_e Game::getGameState()
{
    return m_gameState;
}

void Game::setGameState(GameState_e state)
{
    m_gameState = state;
}
