#ifndef PLAYER_H
#define PLAYER_H

#include <iostream>
#include <vector>

class Player
{
    public:
                                Player(const std::string playerName);
        std::string             getName();
        int                     getIndexHistory();
        void                    setIndexHistory(int index);
        int                     getTeam();
        void                    setTeam(int team);
        int                     getOrder();
        void                    setOrder(int order);
        void                    addPieceOnBoard(int piece);
        void                    removePiecefromBoard(int piece);
        void                    displayPiece(int piece);
        void                    displayPiece2(int piece);
        std::string             getDisplayed();
        std::string             getBoard();
        std::string             getSection(std::vector<int> section);
        std::string             responseNameTeamOrder();
        std::string             responseFullPlayer();
    protected:
        std::string             m_name;
        int                     m_teamId;
        int                     m_order;
        int                     m_indexHistory;
        int                     m_points;
        std::vector<int>        m_board;
        std::vector<int>        m_displayed;
        std::vector<int>        m_displayed2;
    private:
};

#endif // PLAYER_H
