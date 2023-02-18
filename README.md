# 헤르메스

## 프로젝트 소개

---

- 엔진 : Unity 4.21f1
- 목적 : 모바일 게임 제작
- 개발 기간 : 2021.12 ~ 2022.5
- 개발 인원 : 3명

---

## 목표

---

- AI 구현
- 데이터 베이스와 클라이언트 연동 시스템 구현
- 착용 아이템에 따른 외형 변화

---


### 요약

---

* 협업 과정에서 기획자의 능률을 증가시키기 위한 방법의 필요성 증가

  * 시각적인 방법을 통해서 세밀한 조정을 돕는 것이 효율적이라고 판단

    * Gizmo와 Mesh를 통한 편의 기능 제작

* AI의 동작 방식을 유한상태기계(FSM)으로 구현

  * Register State를 통해 추후 다양한 상태를 쉽게 추가할 수 있도록 디자인
  
* 플레이 환경(안드로이드)을 고려하여 Scripatble Object를 통한 최적화

* DB와 데이터 공유 및 저장

---


---

[![image](https://user-images.githubusercontent.com/70616640/219870856-4207aae8-70ec-4342-a7ef-7578cba6c230.png)](https://youtu.be/aySmGnA8aYs)

---

- 장르 : 방치형 모바일 전략 시뮬레이션
- 플랫폼 : 안드로이드

---

### 주요 기능

#### AI

* 유한상태기계(FSM)를 통하여 상태 관리
* [FSM](https://github.com/Black-Developer/portfolio/blob/main/Hermes/Scripts/AI/AiStateMachine.cs)

  * State는 Interface를 통하여 구현
  
  * state.cs
```c#
  public enum AiStateID
  {
    Idle,
    ChaseTarget,
    Targeting,
    Attack,
    Hit,
    Move,
    Die,
    GetItem
  }
  public interface State
  {
    AiStateID GetID();
    public void Enter(AiAgent agent);
    public void UpdateState(AiAgent agent);
    public void Exit(AiAgent agent);
  }
```

  * 대기, 추격, 공격등 다양한 상태 구현

  * Register State를 통해 AI에게 필요한 기능을 쉽게 추가 가능

![image](https://user-images.githubusercontent.com/70616640/219872802-160b10c8-469f-42d4-b4df-26306b660b63.png)

* **일정 범위 탐지**

  * 적과 아이템을 탐지

  * 벽, 장애물로 막혀있는 경우 적을 탐지하지 않음

  * 유동적으로 탐색 범위를 지정할 수 있음

#### 외형

![image](https://user-images.githubusercontent.com/70616640/219874713-eb942d59-d183-4b4b-9508-49aa8aa0ba53.png)

![image](https://user-images.githubusercontent.com/70616640/219874740-40ac0988-41fa-4a5e-a057-3b798924c37d.png)

* 장착 아이템 교체에 따른 외형 변화

  * 특정 아이템을 장착하는 경우 외형 변화

  * 캐릭터 고유의 외형의 경우 변경되지 않음

* 장착 아이템과 인벤토리의 상호작용

  * 아이템을 장착하는 경우 인벤토리에서 해당 아이템의 수량이 감소

  * 아이템을 해제하는 경우 인벤토리에서 해당 아이템의 수량이 증가


* 장착한 아이템에 따른 캐릭터의 능력치 변화

  * 장착한 아이템의 능력치가 캐릭터에게 적용

#### 데이터 베이스와 클라이언트 연동 시스템

* DB를 통하여 플레이어의 캐릭터들의 정보(외형, 장비, 능력치) 저장

  * 캐릭터 뽑기를 통해 생성된 랜덤한 외형의 캐릭터를 DB에 저장
  
```C#

  public class CharacterDrawButton : MonoBehaviour
  {
    public CharacterSystem characterSystem;
    public EquipmentSystem equipSystem;
    public void CharacterDraw()
    {
        int heroClass;
        heroClass =  Random.Range(1,9);
        (characterSystem.equippeditem, characterSystem.shape) = MyItemListContents.Instance.createMyCharacter(heroClass);
        equipSystem.ChangeEquipment();
        GameObject.Find("Canvas_HeroDraw").GetComponent<InventorySystem>().DrawCharacterInfo_HeroDraw(characterSystem.equippeditem);
    }

    public void SaveCharacter(int slot)
    {
        MyItemListContents.Instance.insertMyCharacter(
            characterSystem.equippeditem,
            characterSystem.shape,
            slot);
    }

  }
  
```

#### 시야

![image](https://user-images.githubusercontent.com/70616640/219872352-77fc1898-fd85-4b82-9fc3-e0cc5f645499.png)

* Mesh, Gizmos를 이용하여 개발 과정에서도 시각적으로 확인할 수 있도록 구현

```C#

private void OnDrawGizmos()
    {
        if (visible)
        {
            if (mesh)
            {
                Gizmos.color = meshColor;
                Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
            }
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = Color.green;
            foreach (GameObject obj in Objects)
            {
                Gizmos.DrawSphere(obj.transform.position, 1.0f);
            }
        }
    }

```

#### 아이템 드랍

![image](https://user-images.githubusercontent.com/70616640/219873915-1936ffc4-8b3e-428c-b9b5-c4f667c222fb.png)

* 아이템 희귀도에 따른 개별적 드랍 확률 구현

  * 아이템 추가가 쉽도록 LIST 사용
  
  * 간헐적으로 몬스터 사망 시 아이템이 드랍되지 않는 버그 발생
  
    * 원인 : 몬스터 사망 시 해당 객체에서 실행중인 Coroutine이 강제로 종료되며 문제 발생
  
    * 해결 : Coroutine Manager를 따로 설정하여 해당 객체의 아이템 드랍 Coroutine 담당
    
    * CoroutineHandler.cs
    
```C#
   
public class CoroutineHandler : MonoBehaviour
{

    private static MonoBehaviour monoInstance;

    [RuntimeInitializeOnLoadMethod]
    private static void Initializer()
    {
        monoInstance = new GameObject($"[{nameof(CoroutineHandler)}]").AddComponent<CoroutineHandler>();
        DontDestroyOnLoad(monoInstance.gameObject);
    }
    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }
    public new static void StopCoroutine(IEnumerator coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }
}

```

---

### 아쉬운 부분

- 불필요한 기능을 추가하는 과정에서 시간 낭비, ex) 캐릭터의 시야를 표시해주는 기능은 자주 사용하지 않았음

- 개발 과정에서 캐릭터 위에 현재 상태 정보를 표시해주는 기능을 넣었으면 더 효율적일 것으로 예상

- 아이템 드랍을 Instatiate, Destroy를 통하여 구현하는 것이 아니라, 오브젝트 풀링을 이용하면 메모리를 더 효율적으로 사용할 수 있었던 것이 아쉬움

- Scriptable Object를 통해 최적화 구현했지만, 부하 테스트를 진행하지 않아서 실제로 최적화가 이루어졌는지 확인하지 못한 것이 아쉬움



# In The Dark

## 프로젝트 소개

---

- 엔진 : Unity 3.3f1
- 목적 : 멀티 게임 제작
- 개발 기간 : 2021.7 ~ 2021.10
- 개발 인원 : 2명

---

## 목표

---

- 클라이언트 제작
- 랜덤 맵 생성 알고리즘 구현
- 실시간 NavMeshPath 생성 지원
- AI 및 게임 시스템 구현

---

### 요약

---

* 프로그래머 2명이 레벨을 디자인하는 것은 효율적이지 못하다고 판단

  * 기획자가 없어도 레벨을 제작할 수 있는 방법 필요

* 자동으로 레벨을 생성하는 것이 가장 효율적이고 좋은 결과물을 가져올 수 있을 것이라고 판단

  * 랜덤 방식 레벨 생성 알고리즘 제작

* 랜덤 생성된 레벨에 AI 경로를 생성하는 과정에서 유니티의 기본 기능으로 해결이 불가능

  * 따라서 확장 기능으로 제공하는 Component를 사용하는 계기가 되었음

  * 외부의 기능을 유니티에서 사용하는 방법에 대하여 학습

---

### Level 생성
[레벨 생성 알고리즘](https://github.com/Black-Developer/portfolio/blob/main/InTheDark/Scripts/LevelGenerator.cs)

![image](https://user-images.githubusercontent.com/70616640/219872435-ba7d4d55-6950-404f-9047-e2527d2c4af0.png)

* prefab을 통하여 미리 준비한 프리셋들로 Level 생성

  * Level의 크기를 직접 지정할 수 있음

  * 맵 안에 존재하는 소모품들의 경우 오브젝트 풀링을 통해 구현

### 실시간 NavMeshPath 생성

![image](https://user-images.githubusercontent.com/70616640/219872478-ca4803a4-1f04-4f4c-aea8-3b5f057f928c.png)

* **Level이 생성된 이후 생성된 Level에 따라서 NavMeshPath 생성**

![image](https://user-images.githubusercontent.com/70616640/219872586-96b138cf-2664-49d4-85c0-75cc82175020.png)

  * Unity에서 별도로 제공하는 Component를 통하여 구현
