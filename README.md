# VideoQrCodeDetector

Para rodar a solução:

- No Windows 11, com Docker Desktop instalado;
- Inicie o Docker Desktop;
- Navegue até a pasta raiz da solução via Powershell;
- Rode o comando `docker-compose up -d --build`;
- A aplicação para envio de vídeo estará disponível em `http://localhost:8080/swagger/index.html`
- Para checar se o vídeo foi salvo no Volume Persistente, rode: `docker exec -it envio_video_api ls /app/videos`
- Para acessar o RabbitMQ acesse `https://localhost:15672`, user `guest`, password `guest`
- Para listar os documentos no MongoDB, roder:
	- `docker exec -it mongo_db mongosh`
	- `use envio_video_db`
	- `db.analises.find().pretty()`
- Para encerrar a aplicação `docker-compose down`