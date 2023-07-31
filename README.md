# **스터디 프로그램** [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fhoos007%2FUnivAssignment-StudyProgram&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
C# 윈도우 폼을 사용한 공부 도움 서비스

컴퓨터로 공부하면서 딴짓을 못하도록 하자는 아이디어를 구현했습니다.

이 프로젝트는 대학교 2학년 과정중 C#을 처음 배운 직후 작성했던 프로젝트 입니다.
## 프로젝트 개요
### 이름
스터디 프로그램

### 기획 배경
컴퓨터로 인터넷 강의를 듣거나 여러가지 공부를 할때 책을 펴서 하는 것 보다 컴퓨터로는 할 수 있는것들이 많다보니 딴짓을 할 확률이 높다고 생각했습니다. 따라서 동기부여를 통해 공부를 돕는 프로그램을 개발하면 좋겠다는 아이디어를 토대로 진행한 프로젝트 입니다.

### 주요 기능
1. 공부와 관련한 프로그램의 동작 시간 체크
2. 동기부여를 위한 캐릭터 육성 시스템

## 프로젝트 기간
2019.06.07 ~ 2019.06.14

## 참여인원
개인 프로젝트
| 이름 | 깃허브 | 담당 역할 및 기능 | 비고 |
| ---- | ---- | ---- | ---- |
| 이효승 | [hoos007](https://github.com/hoos007) | 전체 설계 및 구현 | 신라대학교 컴퓨터소트웨어공학부 2학년 |

## 기술 스택 및 환경
### 환경
<img src="https://img.shields.io/badge/windows-0078D4?style=for-the-badge&logo=windows&logoColor=white"> <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white">

### 개발
<img src="https://img.shields.io/badge/visual studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white"> <img src="https://img.shields.io/badge/C Sharp-239120?style=for-the-badge&logo=csharp&logoColor=white">

## 실행화면
메인화면

<img src="readme_img/메인 페이지.png">

유저관리

<img src="readme_img/유저관리.png">

감지 프로세스 관리

<img src="readme_img/감지 프로세스 관리.png">

로그인 후

<img src="readme_img/로그인 후.png">

공부 프로그램 감지

<img src="readme_img/공부 프로그램 감지.png">

게임 프로세스 감지

<img src="readme_img/게임 프로세스 감지.png">

육성 시스템

<img src="readme_img/육성 시스템.png">

## 기능구현
우선 모든 기능은 기본적으로 txt파일을 사용해 저장기능과 로드기능을 구현합니다.

1. 공부와 관련한 프로그램의 동작 시간 체크
    1. 우선 study.txt 파일에 공부 프로세스 이름을 저장합니다.
    2. 로그인 후 화면에서 시작을 누르면 study.txt내용을 읽어옵니다.
    3. 현재 실행중인 프로세스를 불러오는 함수를 사용해 무한루프를 돌면서 실시간으로 공부 프로세스가 동작중인지 아닌지 체크합니다.
    4. 마찬가지로 game.txt를 통해 게임 프로세스의 이름을 저장하고 읽어와서 무한 반복을 돌면서 게임 프로세스를 감지합니다.
    5. 공부 프로세스가 감지되면 자동으로 내부의 초시계가 시간을 체크합니다. 이후 공부 프로세스가 모두 종료될 때 까지 시간을 측정합니다.
    6. 공부 프로세스가 동작중에 게임 프로세스가 같이 감지되면 초시계를 일시정지합니다. 게임 프로세스가 모두 종료되면 다시 초시계를 제개 합니다.
    7. 모든 공부 프로세스가 종료되면 자동으로 캐릭터 육성 페이지로 넘어갑니다.

2. 동기부여를 위한 캐릭터 육성 시스템
    1. 이미지를 미리 저장하고 불러오는 식으로 동작합니다.
    2. 얼마나 오래 공부했는지를 기준으로 경험치를 받습니다.
    3. 경험치가 쌓이고 레벨이 올라가면 아이템이 해금됩니다.
    4. 현재 저장되어있는 모든 유저들과의 랭크를 보여줍니다.
    5. 이번 세션의 공부시간을 보여줍니다.
    6. 경험치 정도와 레벨을 보여줍니다.
