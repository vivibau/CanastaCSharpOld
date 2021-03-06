#include "Game.h"
#include <stdlib.h>

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

Game::~Game()
{
    for (unsigned int i = 0; i < m_players.size(); i++)
        delete m_players[i];
    for (unsigned int i = 0; i < m_history.size(); i++)
        delete m_history[i];
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

void Game::generateGame()
{
    std::vector<int> tmp;
    tmp.clear();
    for (int i = 8; i < 114; i++)
        tmp.push_back(i);

    m_stack.clear();
    for (int i = 0; i < 106; i++)
    {
        int selectedIndex = rand() % tmp.size();
        m_stack.push_back(tmp[selectedIndex]);
        tmp.erase(tmp.begin() + selectedIndex);
    }

    m_rare = m_stack.back();
    m_stack.pop_back();

    for (int i = 0; i < m_numberOfPlayers; i++)
    {
        for (int j = 0; j < 14; j++)
        {
            m_players[i]->addPieceOnBoard(m_stack.back());
            m_stack.pop_back();
        }
        if (m_players[i]->getIndexHistory() == -1)
            m_players[i]->setIndexHistory(m_history.size());
    }

    m_currentPlayer = (m_currentPlayer + 1) % m_numberOfPlayers;
    m_players[m_currentPlayer]->addPieceOnBoard(m_stack.back());
    m_stack.pop_back();

    m_history.push_back(new Update("", GetFullGame_e, responseFullGame()));
}

std::string Game::responsePlayersNameTeamOrder()
{
    std::string result = "";
    result += (char)m_players.size();

    for (unsigned int i = 0; i < m_players.size(); i++)
        result += m_players[i]->responseNameTeamOrder();

    return result;
}

std::string Game::responseFullGame()
{
    std::string result = "";
    result += (char)m_gameType;
    result += (char)m_gameName.length();
    result += m_gameName;

    result += (char)m_players.size();
    for (unsigned int i = 0; i < m_players.size(); i++)
        result += m_players[i]->responseFullPlayer();

    result += (char)m_gameState;
    result += (char)m_currentPlayer;
    result += (char)m_rare;

    return result;
}

std::string Game::responseHistory(int index)
{
    std::string result = "";
    result += (char)(m_history.size() - index);
    for (unsigned int i = index; i < m_history.size(); i++)
        result += m_history[i]->responseData();

    return result;
}
