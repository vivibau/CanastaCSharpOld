#include "Parser.h"
#include "Data.h"
#include "Player.h"

Parser::Parser(const char* input, ssize_t length)
{
    int gameNameSize;
    int gameNameOffset;

    int playerNameSize;
    int playerNameOffset;

    int operationSize = 1;
    int operationOffset;

    int dataSize;
    int dataOffset;

    m_gameType = (GameType_e)input[0];
    gameNameSize = input[1];
    gameNameOffset = 2;

    m_gameName = "";
    for (int i = gameNameOffset; i < gameNameOffset + gameNameSize; i++)
    {
        m_gameName += input[i];
    }
    playerNameSize = input[gameNameOffset + gameNameSize];
    playerNameOffset = gameNameOffset + gameNameSize + 1;

    m_playerName = "";
    for (int i = playerNameOffset; i < playerNameOffset + playerNameSize; i++)
    {
        m_playerName += input[i];
    }
    operationOffset = playerNameOffset + playerNameSize;

    m_operation = (Operation_e)input[operationOffset];

    dataSize = input[operationOffset + operationSize];
    dataOffset = operationOffset + operationSize + 1;
//    m_dataLength = dataOffset;

    m_data = "";
    for (int i = dataOffset; i < dataOffset + dataSize; i++)
    {
        m_data += input[i];
    }

    m_response = "";
}

Parser::~Parser()
{
}

void Parser::parseData()
{
    switch (m_operation)
    {
        case CreateGame_e:  parseCreateGame();
            std::cout << "create game" << std::endl;
            break;
        case JoinGame_e:    //nothing to parse
            break;
        case AskGameList_e: //nothing to parse
            std::cout << "asked game list" << std::endl;
            break;
        case AskPlayers_e:  //nothing to parse
            std::cout << "asked player list" << std::endl;
            break;
        case AddPlayer_e:   //nothing to parse
            std::cout << "add player" << std::endl;
            break;
        default:
            break;
    }
}

void Parser::parseCreateGame()
{
    m_numberOfPlayers = (int)m_data[0];
    m_numberOfTeams = (int)m_data[1];
}

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
        case CreateGame_e:  updateGameCreateGame(games);
            break;
        case JoinGame_e:    updateGameJoinGame(games);
            break;
        case AskGameList_e: updateGameAskGameList(games);
            break;
        case AskPlayers_e:  updateGameAskPlayers(games);
            break;
        case AddPlayer_e:   updateGameAddPlayer(games);
            break;
        default:
            break;
    }
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

void Parser::updateGameAskGameList(std::vector<Game*>& games)
{
    m_response += (char)games.size();
    for (unsigned int i = 0; i < games.size(); i++)
    {
        m_response += (char)games[i]->getGameName().length();
        m_response += games[i]->getGameName();
    }
}

void Parser::updateGameAskPlayers(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    std::vector<Player*> players = selectedGame->getPlayers();
    unsigned int numberOfPlayers = players.size();

    m_response += (char)numberOfPlayers;
    for (unsigned int i = 0; i < numberOfPlayers; i++)
    {
        m_response += (char)players[i]->getName().length();
        m_response += players[i]->getName();
    }
}

void Parser::updateGameAddPlayer(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        m_response += (char)GameInexistent_e;
        return;
    }

    m_response += (char)selectedGame->addPlayer(m_playerName);
}

std::string Parser::getResponse()
{
    return m_response;
}
