<a name='assembly'></a>
# Study.LabWork2.Abstractions

## Contents

- [ExecutionResultDto\`1](#T-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1')
  - [FailedRequests](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-FailedRequests 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1.FailedRequests')
  - [Responses](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-Responses 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1.Responses')
  - [SuccessfulRequests](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-SuccessfulRequests 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1.SuccessfulRequests')
  - [TotalExecutionTime](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-TotalExecutionTime 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1.TotalExecutionTime')
  - [Version](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-Version 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ExecutionResultDto`1.Version')
- [IRequestService](#T-Study-LabWork2-Abstractions-Feature-Task2-IRequestService 'Study.LabWork2.Abstractions.Feature.Task2.IRequestService')
  - [FetchData(url)](#M-Study-LabWork2-Abstractions-Feature-Task2-IRequestService-FetchData-System-String- 'Study.LabWork2.Abstractions.Feature.Task2.IRequestService.FetchData(System.String)')
  - [FetchDataAsync(url,cancellationToken)](#M-Study-LabWork2-Abstractions-Feature-Task2-IRequestService-FetchDataAsync-System-String,System-Threading-CancellationToken- 'Study.LabWork2.Abstractions.Feature.Task2.IRequestService.FetchDataAsync(System.String,System.Threading.CancellationToken)')
- [IServerRequestApp](#T-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp 'Study.LabWork2.Abstractions.Feature.Task2.IServerRequestApp')
  - [ExecuteRequests\`\`1(servers)](#M-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp-ExecuteRequests``1-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto[]- 'Study.LabWork2.Abstractions.Feature.Task2.IServerRequestApp.ExecuteRequests``1(Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto[])')
  - [GetVersion()](#M-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp-GetVersion 'Study.LabWork2.Abstractions.Feature.Task2.IServerRequestApp.GetVersion')
- [ServerConfigDto](#T-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto')
  - [Body](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Body 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.Body')
  - [Headers](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Headers 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.Headers')
  - [Method](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Method 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.Method')
  - [Name](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Name 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.Name')
  - [TimeoutSeconds](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-TimeoutSeconds 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.TimeoutSeconds')
  - [Url](#P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Url 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.Url')
  - [IsValid()](#M-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-IsValid 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.IsValid')
  - [ToString()](#M-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-ToString 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto.ToString')

<a name='T-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1'></a>
## ExecutionResultDto\`1 `type`

##### Namespace

Study.LabWork2.Abstractions.Feature.Task2.DtoModels

##### Summary

Представляет результат выполнения всех запросов к серверам

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-FailedRequests'></a>
### FailedRequests `property`

##### Summary

Количество неудачных запросов (ошибки сети, таймауты, HTTP ошибки)

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-Responses'></a>
### Responses `property`

##### Summary

Список ответов от серверов

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-SuccessfulRequests'></a>
### SuccessfulRequests `property`

##### Summary

Количество успешных запросов (HTTP статус 2xx)

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-TotalExecutionTime'></a>
### TotalExecutionTime `property`

##### Summary

Общее время выполнения всех запросов

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ExecutionResultDto`1-Version'></a>
### Version `property`

##### Summary

Версия приложения (синхронная/асинхронная)

<a name='T-Study-LabWork2-Abstractions-Feature-Task2-IRequestService'></a>
## IRequestService `type`

##### Namespace

Study.LabWork2.Abstractions.Feature.Task2

##### Summary

Интерфейс для реализации методов опроса другого сервиса

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-IRequestService-FetchData-System-String-'></a>
### FetchData(url) `method`

##### Summary

Отправить запрос к сервису

##### Returns

Ответ от сервиса

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Веб-адрес |

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-IRequestService-FetchDataAsync-System-String,System-Threading-CancellationToken-'></a>
### FetchDataAsync(url,cancellationToken) `method`

##### Summary

Отправить запрос к сервису

##### Returns

Ответ от сервиса

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Веб-адрес |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Токен отмены |

<a name='T-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp'></a>
## IServerRequestApp `type`

##### Namespace

Study.LabWork2.Abstractions.Feature.Task2

##### Summary

Общий интерфейс для всех версий приложения

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp-ExecuteRequests``1-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto[]-'></a>
### ExecuteRequests\`\`1(servers) `method`

##### Summary

Выполняет запросы к серверам

##### Returns

Результат выполнения

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servers | [Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto[]](#T-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto[] 'Study.LabWork2.Abstractions.Feature.Task2.DtoModels.ServerConfigDto[]') | Список конфигураций серверов |

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-IServerRequestApp-GetVersion'></a>
### GetVersion() `method`

##### Summary

Возвращает название версии

##### Returns

Название версии

##### Parameters

This method has no parameters.

<a name='T-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto'></a>
## ServerConfigDto `type`

##### Namespace

Study.LabWork2.Abstractions.Feature.Task2.DtoModels

##### Summary

Представляет конфигурацию сервера для выполнения HTTP запроса

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Body'></a>
### Body `property`

##### Summary

Тело запроса для методов POST/PUT (в формате объекта)

##### Remarks

Будет автоматически сериализован в JSON.
Для GET запросов игнорируется.

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Headers'></a>
### Headers `property`

##### Summary

Заголовки HTTP запроса (опционально)

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Method'></a>
### Method `property`

##### Summary

HTTP метод запроса (GET/POST/PUT/DELETE)

##### Remarks

По умолчанию используется GET

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Name'></a>
### Name `property`

##### Summary

Название сервера (для идентификации в логах)

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-TimeoutSeconds'></a>
### TimeoutSeconds `property`

##### Summary

Таймаут запроса в секундах

##### Remarks

По умолчанию 30 секунд.
При превышении таймаута будет выброшено исключение.

<a name='P-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-Url'></a>
### Url `property`

##### Summary

URL сервера (полный адрес с протоколом)

##### Example

https://jsonplaceholder.typicode.com/users

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-IsValid'></a>
### IsValid() `method`

##### Summary

Проверяет корректность конфигурации

##### Returns

true, если конфигурация валидна

##### Parameters

This method has no parameters.

<a name='M-Study-LabWork2-Abstractions-Feature-Task2-DtoModels-ServerConfigDto-ToString'></a>
### ToString() `method`

##### Summary

Возвращает строковое представление конфигурации сервера

##### Returns

Строка в формате "Название: URL (Метод)"

##### Parameters

This method has no parameters.
