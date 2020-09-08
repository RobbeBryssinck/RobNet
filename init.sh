#!/bin/sh
echo "# HTTPServerExploit" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M master
git remote add origin https://github.com/RobbeBryssinck/HTTPServerExploit.git
git push -u origin master
