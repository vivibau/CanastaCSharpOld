#include "Player.h"

Player::Player(const std::string playerName)
{
    m_name = playerName;
//    m_gameId = gameId;
    m_teamId = -1;
    m_order = -1;
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
