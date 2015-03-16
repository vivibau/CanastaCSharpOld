#include "Parser.h"
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
        case AskStatus_e:   //nothing to parse
            break;
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
        case Broadcast_e: parseBroadcast();
            break;
        case AssignPlayer_e: parseAssignPlayer();
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

void Parser::parseBroadcast()
{
    m_message = "[" + m_playerName + "]: ";
    for (int i = 0; i < (int)m_data[0]; i++)
        m_message += (char)m_data[i + 1];
}

void Parser::parseAssignPlayer()
{
    m_selectedPlayer = "";
    for (int i = 0; i < m_data[0]; i++)
        m_selectedPlayer += m_data[i + 1];

    m_selectedTeam = m_data[m_data[0] + 2];
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
        case AskStatus_e:       updateGameAskStatus(games);
            break;
        case CreateGame_e:      updateGameCreateGame(games);
            break;
        case JoinGame_e:        updateGameJoinGame(games);
            break;
        case AskGameList_e:     updateGameAskGameList(games);
            break;
        case AskPlayers_e:      updateGameAskPlayers(games);
            break;
        case AddPlayer_e:       updateGameAddPlayer(games);
            break;
        case Broadcast_e:       updateGameBroadcast(games);
            break;
        case AssignPlayer_e:    updateGameAssignPlayer(games);
            break;
        default:
            break;
    }
}

void Parser::updateGameAskStatus(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    if (selectedGame == NULL)
    {
        // return list of games
        int numberOfGames = games.size();
        m_response += (char)numberOfGames;
        for (int i = 0; i < numberOfGames; i++)
        {
            std::string gameName = games[i]->getGameName();
            m_response += (char)gameName.length();
            m_response += gameName;
        }
        return;
    }

    if (selectedGame->getGameState() == WaitingForPlayers_e)
    {
        // return list of players
        std::vector<Player*>& players = selectedGame->getPlayers();
        int numberOfPlayers = players.size();
        m_response += (char)numberOfPlayers;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            std::string playerName = players[i]->getName();
            m_response += (char)playerName.length();
            m_response += playerName;
            m_response += (char)players[i]->getTeam();
        }
        return;
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
        newGame->update(m_playerName, m_operation, "");
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
    selectedGame->update(m_playerName, m_operation, "");
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
    selectedGame->update(m_playerName, m_operation, "");
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

void Parser::updateGameAssignPlayer(std::vector<Game*>& games)
{
    Game* selectedGame = getSelectedGame(games);
    std::vector<Player*>& players = selectedGame->getPlayers();
    for (unsigned int i = 0; i < players.size(); i++)
        if (players[i]->getName() == m_selectedPlayer)
        {
            players[i]->setTeam(m_selectedTeam);
            break;
        }
}

std::string Parser::getResponse()
{
    return m_response;
}
