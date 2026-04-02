# Dockerfile multietapa para imagen ultraliviana de AutoCita.Api
# Etapa 1: Build - Usar imagen SDK completa para compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copiar archivos de proyecto primero para aprovechar cache de Docker
COPY *.csproj ./
COPY *.sln ./

# Restaurar dependencias
RUN dotnet restore Backend.sln

# Copiar el resto del código fuente
COPY . ./

# Compilar en modo Release con optimizaciones
RUN dotnet publish Backend.sln -c Release -o /app/publish \
    --no-restore \
    --self-contained false \
    /p:PublishTrimmed=false \
    /p:PublishSingleFile=false

# Etapa 2: Runtime - Imagen ultraliviana para producción
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime

# Crear usuario no privilegiado para seguridad
RUN addgroup -g 1001 -S appgroup && \
    adduser -S appuser -u 1001 -G appgroup

# Instalar dependencias mínimas para PostgreSQL
RUN apk add --no-cache \
    icu-libs \
    tzdata && \
    rm -rf /var/cache/apk/*

# Configurar zona horaria
ENV TZ=UTC

# Establecer directorio de trabajo
WORKDIR /app

# Copiar archivos compilados desde etapa de build
COPY --from=build /app/publish .

# Cambiar propietario de archivos al usuario no privilegiado
RUN chown -R appuser:appgroup /app

# Cambiar a usuario no privilegiado
USER appuser

# Configurar variables de entorno para optimización
ENV ASPNETCORE_ENVIRONMENT=Development \
    ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_gcServer=1 \
    DOTNET_GCRetainVM=1

# Exponer puerto
EXPOSE 8080

# Comando de inicio
ENTRYPOINT ["dotnet", "AutoCita.Api.dll"]

# Etiquetas para metadatos
LABEL maintainer="AutoCita Team" \
      version="1.0" \
      description="AutoCita API - Ultra lightweight production image"