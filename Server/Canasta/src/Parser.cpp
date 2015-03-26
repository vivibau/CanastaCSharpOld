#include "Parser.h"
#include "Player.h"

std::string readString(char** source)
{
    std::string result = "";
    int length = (*source)[0];
    (*source)++;

    for (int i = 0; i < length; i++)
    {
        result += (*source)[i];
    }

    (*source) += length;
    return result;
}

int readInt(char** source)
{
    int result = (*source)[0];

    (*source)++;
    return result;
}

std::string getUpdates(Game* selectedGame, std::string playerName)
{
    std::string result = "";
    std::vector<Player*>& players = selectedGame->getPlayers();
    int numberOfPlayers = players.size();

    int index = -1;
    int playerIndex = -1;
    for (int i = 0; i < numberOfPlayers; i++)
        if (players[i]->getName() == playerName)
        {
            playerIndex = i;
            index = players[i]->getIndexHistory();
            break;
        }

    if (index > -1)
    {
        result += selectedGame->responseHistory(index);
        players[playerIndex]->setIndexHistory(-1);
    }
    else
        result += (char)0;

    return result;
}

std::string Parser::responseGameList(std::vector<Game*>& games)
{
    std::string result = "";
    int numberOfGames = games.size();
    result += (char)numberOfGames;
    for (int i = 0; i < numberOfGames; i++)
    {
        std::string gameName = games[i]->getGameName();
        result += (char)gameName.length();
        result += gameName;
    }

    return result;
}

Parser::Parser(char* input, ssize_t length)
{
    m_gameType      = (GameType_e)readInt(&input);
    m_gameName      = readString(&input);
    m_playerName    = readString(&input);
    m_operation     = (Operation_e)readInt(&input);
    m_data          = readString(&input);

    m_response = "";
}

Parser::~Parser()
{
}

void Parser::parseData()
{
    switch (m_operation)
    {
        case AskGameList_e:
            break;
        case AskPlayerList_e:
            break;
        case AskStatus_e:
            break;
        case AssignPlayer_e:    parseAssignPlayer();
            break;
        case Broadcast_e:       parseBroadcast();
            break;
        case CreateGame_e:      parseCreateGame();
            break;
        case DeleteGame_e:
            break;
        case JoinGame_e:
            break;
        case LeaveGame_e:
            break;
        case StartGame_e:
            break;
        default:
            break;
    }
}

void Parser::parseAssignPlayer()
{
    m_selectedPlayer = "";
    for (int i = 0; i < m_data[0]; i++)
        m_selectedPlayer += m_data[i + 1];

    m_selectedTeam = (int)m_data[m_data[0] + 1];
    m_selectedOrder = (int)m_data[m_data[0] + 2];
}

void Parser::parseBroadcast()
{
    m_message = "[" + m_playerName + "]: ";
    for (int i = 0; i < (int)m_data[0]; i++)
        m_message += (char)m_data[i + 1];
}

void Parser::parseCreateGame()
{
    m_numberOfPlayers = (int)m_data[0];
    m_numberOfTeams = (int)m_data[1];
}

// helper
Game* Parser::getSelectedGame(std::vector<Game*>& games)
{
    for (unsigned int i = 0; i < games.size(); i++)
        if (games[i]->getGameName() == m_gameName)
            return games[i];
    return NULL;
}

void Parser::updateGame(std::vector<Game*>& games)
{
    switch (m_operation)
    {
        case AskGameList_e:     updateGameAskGameList(games);
            break;
        case AskPlayerList_e:   updateGameAskPlayerListAndUpdates(games);
            break;
        case AskStatus_e:       updateGameAskStatus(games);
            break;
        case AssignPlayer_e:    updateGameAssignPlayer(games);
            break;
        case Broadcast_e:       updateGameBroadcast(games);
            break;
        case CreateGame_e:      updateGameCreateGame(games);
            break;
        case DeleteGame_e:      updateGameDeleteGame(games);
            break;
        case JoinGame_e:        updateGameJoinGame(games);
            break;
        case LeaveGame_e:       updateGameLeaveGame(games);
            break;
        case StartGame_e:       updateGameStartGame(games);
            break;
        default:
            break;
    }
}

void Parser::updateGameAskGameList(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        if (m_gameName != "")
        {
            // game has been deleted
            m_response += (char)GameInexistent_e;
            return;
        }

        // return list of games
        m_response += (char)OK_e;
        m_response += (char)StateUnknown_e;
        m_response += responseGameList(games);
        return;
    }
}

void Parser::updateGameAskPlayerListAndUpdates(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    m_response += (char)OK_e;
    m_response += (char)selectedGame->getGameState();
    m_response += selectedGame->responsePlayersNameTeamOrder();
    m_response += getUpdates(selectedGame, m_playerName);

    return;
}

void Parser::updateGameAskStatus(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        if (m_gameName != "")
        {
            // game has been deleted
            m_response += (char)GameInexistent_e;
            return;
        }

        // return list of games
        m_response += (char)OK_e;
        m_response += (char)StateUnknown_e;
        m_response += responseGameList(games);
        return;
    }

    std::vector<Player*>& players = selectedGame->getPlayers();
    int numberOfPlayers = players.size();

    if (selectedGame->getGameState() == WaitingForPlayers_e)
    {
        // return list of players
        m_response += (char)OK_e;
        m_response += (char)selectedGame->getGameState();
        m_response += selectedGame->responsePlayersNameTeamOrder();
    }

    if (m_response.size() == 0)
    {
        m_response += (char)OK_e;
        m_response += (char)selectedGame->getGameState();
    }

    int index = -1;
    int playerIndex = -1;
    for (int i = 0; i < numberOfPlayers; i++)
        if (players[i]->getName() == m_playerName)
        {
            playerIndex = i;
            index = players[i]->getIndexHistory();
            break;
        }

    if (index > -1)
    {
        m_response += selectedGame->responseHistory(index);
        players[playerIndex]->setIndexHistory(-1);
    }
    else
        m_response += (char)0;
    return;
}

void Parser::updateGameAssignPlayer(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    std::vector<Player*>& players = selectedGame->getPlayers();
    for (unsigned int i = 0; i < players.size(); i++)
        if (players[i]->getName() == m_selectedPlayer)
        {
            if (players[i]->getOrder() == 100 || players[i]->getOrder() == m_selectedOrder)
            {
                players[i]->setOrder(m_selectedOrder);
                players[i]->setTeam(m_selectedTeam);
                m_response += (char)OK_e;
                return;
            }

            if (m_selectedTeam == players[i]->getTeam())
            {
                for (unsigned int ii = 0; ii < players.size(); ii++)
                    if (players[ii]->getOrder() == m_selectedOrder)
                    {
                        players[ii]->setOrder(players[i]->getOrder());
                        players[i]->setOrder(m_selectedOrder);
                        m_response += (char)OK_e;
                        return;
                    }
            }
            else
            {
                players[i]->setOrder(m_selectedOrder);
                players[i]->setTeam(m_selectedTeam);
            }

            break;
        }
    m_response += (char)OK_e;
}

void Parser::updateGameBroadcast(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    m_response += (char)OK_e;
    selectedGame->update(m_playerName, m_operation, m_data);
}

void Parser::updateGameCreateGame(std::vector<Game*>& games)
{
    if (getSelectedGame(games) == NULL)
    {
        Game* newGame = new Game(m_gameType, m_gameName, m_numberOfPlayers, m_numberOfTeams);
        newGame->addPlayer(m_playerName);
        games.push_back(newGame);
        m_response += (char)OK_e;
        return;
    }

    m_response += (char)GameExists_e;
}

void Parser::updateGameDeleteGame(std::vector<Game*>& games)
{
    Game* selectedGame = NULL;
    int deletionIndex = -1;
    for (unsigned int i = 0; i < games.size(); i++)
        if (games[i]->getGameName() == m_gameName)
        {
            deletionIndex = i;
            selectedGame = games[i];
        }

    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    games.erase(games.begin() + deletionIndex);
    delete selectedGame;

    m_response += (char)OK_e;
    return;
}

void Parser::updateGameJoinGame(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    m_response += (char)selectedGame->addPlayer(m_playerName);
}

void Parser::updateGameLeaveGame(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    m_response += (char)selectedGame->removePlayer(m_playerName);
}

void Parser::updateGameStartGame(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }
    selectedGame->setGameState(InProgress_e);
    m_response += (char)OK_e;
    selectedGame->generateGame();
}

std::string Parser::getResponse()
{
    return m_response;
}
