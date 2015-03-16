/*#include <iostream>

using namespace std;

int main()
{
    return 0;
}
*/
#include <stdio.h>
#include <stdlib.h>
#include <vector>
#include <iostream>
#include "TCPAcceptor.h"
#include "Game.h"
#include "Parser.h"
#include "Includes.h"

int main(int argc, char** argv)
{


    if (argc < 2 || argc > 4) {
        printf("usage: server <port>\n");
        exit(1);
    }

    TCPStream* stream = NULL;
    TCPAcceptor* acceptor = NULL;

    std::vector<Game*> games;

    acceptor = new TCPAcceptor(atoi(argv[1]));

    if (acceptor->start() == 0) {
        while (1) {
            stream = acceptor->accept();
            if (stream != NULL) {
                ssize_t len;
                char line[1024];
                while ((len = stream->receive(line, sizeof(line))) > 0) {
                    Parser* parser = new Parser(line, len);
                    parser->parseData();

                    parser->updateGame(games);

                    std::string response = parser->getResponse();
                    stream->send(response.c_str(), response.length());
                    if (parser) delete(parser);
                }
                delete stream;
            }
        }
//        if (acceptor) delete(acceptor);
    }
    exit(0);
}
