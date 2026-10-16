# Deep Down — Unity Gameplay Code

<p align="center">
  <strong>3D 공포 퍼즐 게임 「Deep Down」의 Unity C# gameplay code</strong><br/>
  졸업작품에서 팀이 작성한 게임 로직을 살펴보고, 일부 코드를 유지보수하기 쉽게 정리하는 저장소입니다.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Language-C%23-512BD4?logo=csharp&logoColor=white" alt="C Sharp"/>
  <img src="https://img.shields.io/badge/Engine-Unity%202018.2.10f1-000000?logo=unity&logoColor=white" alt="Unity"/>
  <img src="https://img.shields.io/badge/Project-Graduation%20Work-6C5CE7" alt="Graduation work"/>
</p>

## 프로젝트 소개

**Deep Down**은 팀으로 제작한 3D 공포 퍼즐 게임입니다. 이 저장소는 전체 게임 프로젝트가 아닌, 팀에서 작성한 Unity C# 스크립트를 중심으로 보관합니다. 대용량 모델·사운드·영상 및 외부 에셋은 포함하지 않습니다.

| 항목 | 내용 |
|---|---|
| 프로젝트 유형 | 졸업작품 / 팀 프로젝트 |
| 장르 | 3D 공포 퍼즐 게임 |
| 엔진 | Unity 2018.2.10f1 |
| 코드 | `Assets/02. Scripts` |
| 주요 언어 | C# |

## 코드 구성

| 폴더 | 주요 역할 |
|---|---|
| `KDH` | 버튼·자물쇠 퍼즐, 문·조명 제어, 적과 스테이지 연출 |
| `PDS` | 게임 상태와 사운드 관리, 공통 이벤트, 시계 퍼즐 |
| `YSM` | 카메라 상호작용, UI, 인벤토리 및 기억 퍼즐 |
| `Struct` | 팀 공용 열거형과 상태 정의 |
| `Editor` | Unity Inspector 확장 |

## 유지보수 및 리팩터링 포인트

기존 게임 동작과 Unity Inspector 연결을 유지하면서 다음 코드 흐름을 중심으로 검토합니다.

- 빠른 연속 입력에 따른 애니메이션 중복 실행 방지
- 잠금 해제 상태와 효과음의 반복 처리를 한 번만 발생하도록 정리
- 조명 컴포넌트 조회를 필요한 시점에 수행해 프레임별 반복 조회 줄이기

이 저장소는 코드 검토와 변경 이력 확인에 초점을 둡니다. 게임 실행 장면과 에셋을 포함한 전체 프로젝트는 별도로 필요합니다.

## 실행 환경 및 범위

- Unity 2018.2.10f1 기준으로 작성된 스크립트입니다.
- 스크립트를 기존 Deep Down Unity 프로젝트의 `Assets/02. Scripts`에 배치해 사용합니다.
- 이 저장소만으로는 씬, 프리팹, 모델, 사운드 등 게임을 실행하는 데 필요한 모든 리소스를 제공하지 않습니다.
- 프로젝트의 Unity 버전을 변경할 경우 기존 Inspector 직렬화 데이터와 컴포넌트 연결을 함께 확인해야 합니다.

## 프로젝트 구조

```text
.
└─ Assets/
   └─ 02. Scripts/
      ├─ Editor/       # Inspector 확장
      ├─ KDH/          # 퍼즐·문·조명·스테이지 로직
      ├─ PDS/          # 공통 상태·이벤트·사운드 로직
      ├─ Struct/       # 공통 데이터 타입
      └─ YSM/          # UI·카메라·인벤토리·기억 퍼즐
```

---

졸업작품의 게임플레이 코드를 보존하고, 기존 동작을 이해하며 점진적으로 개선하기 위한 포트폴리오 저장소입니다.
