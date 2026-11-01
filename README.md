# Morbido

Unity UI 프로토타입 저장소입니다. 대사를 한 글자씩 출력하는 타이핑 연출과, 마우스로 돌리는 슬롯식 숫자 스크롤 UI를 실험했습니다.

## 소개

- 커밋 3건이 2020-07-14부터 2020-07-21까지 이어졌고, 마지막 커밋 메시지는 "애니메이션 실험"입니다. 저장소 설명은 없습니다.
- 스크립트는 모두 `Assets/Dummy(Minkyu)/` 아래에 있습니다. `Dummy(...)`는 작업자별로 폴더를 나누는 이름으로 보이지만, 다른 폴더나 참여 인원은 저장소에서 확인되지 않습니다. 프로젝트 전체의 목적도 적혀 있지 않습니다.

## 핵심 구현

### 대사 타이핑 연출 (`Dialog.cs`)

`MsgInfo`(`Msg`, `Delay`)를 직렬화 가능한 클래스로 두고 인스펙터에서 대사 목록을 채웁니다. `Dialog`는 `Text` 컴포넌트에 코루틴 `MsgQueue_Start`로 글자를 한 글자씩 붙입니다.

```csharp
for (int i = 0; i < Temp_Msg.Length; i++)
{
    yield return new WaitForSeconds(Delay);   // 글자 사이 간격은 대사마다 지정
    text.text += Temp_Msg[i];
}
```

현재 `Start`는 목록의 첫 번째 대사(`MsgQueue[0]`)만 재생합니다.

### 숫자 스크롤 (`PriceScroll.cs`, `CenterLine.cs`)

세로로 넘어가는 숫자 목록을 슬롯머신처럼 돌리는 UI입니다.

- 드래그: 마우스를 누르는 동안 이전 프레임과 현재 프레임의 Y 차이를 `Scroll_By`로 넘겨 속도(`Power`)로 삼고, 놓으면 `Power`가 프레임마다 `0.99`배로 줄어드는 관성 스크롤이 됩니다.
- 무한 스크롤: 같은 높이의 두 스크롤 오브젝트(`ScrollObject01/02`)를 쓰고, 한쪽이 아래 한계(`Bottom`)를 넘으면 2000만큼 위로, 위 한계(`Top`)를 넘으면 아래로 옮겨 이어 붙입니다.
- 정렬 보정: 스크롤이 0.5초 이상 지나 속도가 0.4 미만으로 떨어지면 `CenterLine`이 감지한 항목(`OnTriggerStay2D`로 중앙선과 겹친 `RectTransform`)과 중앙선의 거리를 계산하고, `Mathf.SmoothStep`으로 그 거리를 줄여 항목이 중앙선에 딱 맞게 멈춥니다.

## 프로젝트 구조

| 경로 | 내용 |
| --- | --- |
| `Assets/Dummy(Minkyu)/Dialog/` | `Dialog.cs`, 실험 씬 `Dialog.unity` |
| `Assets/Dummy(Minkyu)/NumberScroll/` | `PriceScroll.cs`, `CenterLine.cs`, 실험 씬 `PriceScrollTest.unity` |
| `Assets/Dummy(Minkyu)/Title.unity`, `Image.controller`, `New Animation.anim` | 타이틀 씬과 애니메이션 실험 |
| `Assets/Formal/Scene/TitleScene.unity` | 정식 구조용으로 만든 타이틀 씬 (그 외 `Formal/` 폴더는 대부분 비어 있음) |

## 기술 스택

- Unity 2019.4.3f1 (`ProjectSettings/ProjectVersion.txt`), C#, uGUI
- 패키지: 2D Sprite/Tilemap, TextMeshPro 2.0.1, Unity Ads/Purchasing/Analytics 기본 패키지 (`Packages/manifest.json`)

## 실행 방법

1. Unity Hub에서 Unity 2019.4.3f1을 설치합니다.
2. 저장소를 프로젝트 폴더로 열고 `Assets/Dummy(Minkyu)/Dialog/Dialog.unity` 또는 `Assets/Dummy(Minkyu)/NumberScroll/PriceScrollTest.unity`를 재생합니다.

## 현재 상태

초기 프로토타입 단계에서 중단되었고 2020-07-21 이후 갱신이 없습니다.

- `PriceScroll.Scroll_To(int)`는 본문이 비어 있어 원하는 숫자로 이동하는 기능은 없습니다.
- 정렬 보정은 `CenterLine.CollisionObject`가 한 번이라도 설정되어야 동작합니다.
- 이 문서는 당시 환경(Unity 2019.4.3f1) 기준으로 코드를 읽고 정리한 것이며, 현재 환경에서 다시 열어 확인하지는 않았습니다.
