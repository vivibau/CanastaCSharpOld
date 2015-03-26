#include "Update.h"

Update::Update(std::string playerName, Operation_e operation, std::string data)
{
    m_playerName = playerName;
    m_operation = operation;
    m_data = data;
}

std::string Update::getPlayerName()
{
    return m_playerName;
}

Operation_e Update::getOperationType()
{
    return m_operation;
}

std::string Update::getData()
{
    return m_data;
}

std::string Update::responseData()
{
    std::string result = "";
    result += (char)m_playerName.length();
    result += m_playerName;
    result += (char)m_operation;
    result += (char)m_data.length();
    result += m_data;

    return result;
}
