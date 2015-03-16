#ifndef UPDATE_H
#define UPDATE_H

#include <iostream>
#include "Includes.h"

class Update
{
    public:
        Update(std::string playerName, Operation_e operation, std::string data);
    protected:
    private:
        std::string         m_playerName;
        Operation_e         m_operation;
        std::string         m_data;
};

#endif // UPDATE_H
