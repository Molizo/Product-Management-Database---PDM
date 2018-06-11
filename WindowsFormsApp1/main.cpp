#include <iostream>
#include <fstream>
#include <Windows.h>
#include <string.h>

using namespace std;

bool checkUserLoginInfo()
{
	string temp, enteredUsername, enteredPassword;
	ifstream in("textBridgeSend.tmp");
	in >> temp >> enteredUsername >> enteredPassword;
	cout<<temp<<enteredUsername<<enteredPassword;

}

int main()
{
	string menu;
	ifstream in("textBridgeSend.tmp");
	in >> menu;
	in.close();
	cout<<menu;
	if (menu == "Login")
		if(checkUserLoginInfo() == true)
        {
            ofstream out("textBridgeReceive.tmp");
            out<<"Welcome to the database!\nYou are logged in.";
            out.close();
        }
    return 0;
}

