#include "Player.h"

Player::Player(const std::string playerName)
{
    m_name = playerName;
//    m_gameId = gameId;
    m_teamId = 100; // some big number, there will never be 100 teams in one game :)
    m_order = 100; // some big number, there will never be 100 players in one game :)
    m_indexHistory = -1;
//    m_dirty = false;
}

std::string Player::getName()
{
    return m_name;
}

int Player::getIndexHistory()
{
    return m_indexHistory;
}

void Player::setIndexHistory(int index)
{
    m_indexHistory = index;
}

int Player::getTeam()
{
    return m_teamId;
}

void Player::setTeam(int team)
{
    m_teamId = team;
}

int Player::getOrder()
{
    return m_order;
}

void Player::setOrder(int order)
{
    m_order = order;
}
