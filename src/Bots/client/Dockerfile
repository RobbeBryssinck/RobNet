FROM python:3.8

WORKDIR /code

COPY Bots/client/requirements.txt .

RUN pip install -r requirements.txt

COPY Bots/client .

CMD [ "python", "./Bots/client/bot.py" ]