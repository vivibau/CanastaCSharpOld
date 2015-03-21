#include "Player.h"
#include <vector>

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

void Player::addPieceOnBoard(int piece)
{
    m_board.push_back(piece);
}

void Player::removePiecefromBoard(int piece)
{
    int index = -1;
    for (unsigned int i = 0; i < m_board.size(); i++)
        if (piece == m_board[i])
        {
            index = i;
            break;
        }
    m_board.erase(m_board.begin() + index);
}

void Player::displayPiece(int piece)
{
    m_displayed.push_back(piece);
}

void Player::displayPiece2(int piece)
{
    m_displayed2.push_back(piece);
}

std::string Player::getDisplayed()
{
    return "";
}

std::string Player::getBoard()
{
    std::string board = "";

    board += (char)m_board.size();
    for (unsigned int i = 0; i < m_board.size(); i++)
        board += (char)m_board[i];

    return board;
}
