#ifndef UPDATE_H
#define UPDATE_H

#include <iostream>
#include "Includes.h"

class Update
{
    public:
        Update(std::string playerName, Operation_e operation, std::string data);
        std::string getPlayerName();
        Operation_e getOperationType();
        std::string getData();
        std::string responseData();
    protected:
    private:
        std::string         m_playerName;
        Operation_e         m_operation;
        std::string         m_data;
};

#endif // UPDATE_H
