#include "Update.h"

Update::Update(std::string playerName, Operation_e operation, std::string data)
{
    m_playerName = playerName;
    m_operation = operation;
    m_data = data;
}
