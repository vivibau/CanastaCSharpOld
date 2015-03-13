#ifndef PLAYER_H
#define PLAYER_H

#include <iostream>

class Player
{
    public:
                                Player() {};
                                Player(const std::string playerName);
        std::string             getName();
    protected:
        std::string             m_name;
//        std::string m_gameId;
        int                     m_teamId;
        int                     m_order;
//        bool m_dirty;
    private:
};

#endif // PLAYER_H
