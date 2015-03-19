#ifndef PLAYER_H
#define PLAYER_H

#include <iostream>

class Player
{
    public:
                                Player() {};
                                Player(const std::string playerName);
        std::string             getName();
        int                     getIndexHistory();
        void                    setIndexHistory(int index);
        int                     getTeam();
        void                    setTeam(int team);
        int                     getOrder();
        void                    setOrder(int order);
    protected:
        std::string             m_name;
//        std::string m_gameId;
        int                     m_teamId;
        int                     m_order;
        int                     m_indexHistory;
//        bool m_dirty;
    private:
};

#endif // PLAYER_H
