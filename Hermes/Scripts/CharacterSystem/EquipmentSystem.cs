using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    // list of enabed objects on character
    //[HideInInspector]
    public List<GameObject> enabledObjects = new List<GameObject>();
    public CharacterSystem characterSystem;
    public AiAgent agent;
    public List<Equipment> equipments;

    public Accessary accessaryL;
    public Accessary accessaryR;
    public Helmet helmet;
    public Clothes clothes;
    public Pants pants;
    public Bag bag;
    public Weapon weapon;

    // character object lists
    // character list
    //[HideInInspector]
    public CharacterObjectGroups character;
    private void Start()
    {
        characterSystem = GetComponent<CharacterSystem>();
        // rebuild all lists
        BuildLists();

        // disable any enabled objects before clear
        if (enabledObjects.Count != 0)
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        // clear enabled objects list
        enabledObjects.Clear();

        //ChangeEquipment();

        equipments.Add(accessaryL);
        equipments.Add(accessaryR);
        equipments.Add(helmet);
        equipments.Add(clothes);
        equipments.Add(pants);
        equipments.Add(weapon);

        agent = GetComponent<AiAgent>();
        agent.weapon = weapon;
        ChangeEquipment();
    }
    public void Update()
    {
        if (agent.weapon == null)
        {
            agent.weapon = weapon;
        }
    }
    public void LoadStats(int _hp, int _damage, int _armor, float _attackSpeed, float _critical, float _accuracy)
    {
        foreach(Equipment equipment in equipments)
        {
            equipment.LoadStats(_hp, _damage, _armor, _attackSpeed, _critical, _accuracy);
        }
    }
    // character randomization method
    public void ChangeEquipment()
    {
        // disable any enabled objects before clear
        if (enabledObjects.Count != 0)
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        // clear enabled objects list (all objects now disabled)
        enabledObjects.Clear();
        ChangeHair(
            characterSystem.shape.hairId);
        ChangeEyebrow(
            characterSystem.shape.eyebrowId);
        ChangeFace(
            characterSystem.shape.faceId);
        ChangeFacialHair(
            characterSystem.shape.facialId);
        ChangeArmLR(
            characterSystem.shape.armLRId);
        ChangeArmLL(
            characterSystem.shape.armLLId);
        ChangeArmUR(
            characterSystem.shape.armURId);
        ChangeArmUL(
            characterSystem.shape.armULId);
        ChangeHandL(
            characterSystem.shape.handLId);
        ChangeHandR(
            characterSystem.shape.handRId);
        ChangeLegL(
            characterSystem.shape.legLId);
        ChangeLegR(
            characterSystem.shape.legRId);
        ChangeHelmet(
            characterSystem.equippeditem.helmetId);
        ChangePants(
            characterSystem.equippeditem.pantsId);
        ChangeClothes(
            characterSystem.equippeditem.clothesId);
        ChangeBag(
            characterSystem.equippeditem.bagId
            );
        ChangeLeftAccessary(
            characterSystem.equippeditem.accessaryLId);
        ChangeRightAccessary(
            characterSystem.equippeditem.accessaryRId);
        ChangeWeapon(
            characterSystem.equippeditem.weaponId);
    }
    #region FIXED_PART
    void ChangeHair(int index)
    {
        foreach (GameObject obj in character.all_Hair)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeEyebrow(int index)
    {
        foreach(GameObject obj in character.eyebrow)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeFace(int index)
    {
        foreach(GameObject obj in character.headAllElements)
        {
            if(obj.GetComponent<BodyPartBase>() != null)
            {
                if(obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeFacialHair(int index)
    {
        foreach(GameObject obj in character.facialHair)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeArmLR(int index)
    {
        foreach(GameObject obj in character.arm_Lower_Right)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeArmLL(int index)
    {
        foreach(GameObject obj in character.arm_Lower_Left)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeArmUR(int index)
    {
        foreach(GameObject obj in character.arm_Upper_Right)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeArmUL(int index)
    {
        foreach(GameObject obj in character.arm_Upper_Left)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }

    void ChangeHandL(int index)
    {
        foreach(GameObject obj in character.hand_Left)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeHandR(int index)
    {
        foreach(GameObject obj in character.hand_Right)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeLegL(int index)
    {
        foreach(GameObject obj in character.leg_Left)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    void ChangeLegR(int index)
    {
        foreach(GameObject obj in character.leg_Right)
        {
            if (obj.GetComponent<BodyPartBase>() != null)
            {
                if (obj.GetComponent<BodyPartBase>().index == index)
                {
                    ActivateItem(obj);
                }
            }
        }
    }
    #endregion FIXED_PART

    #region VARIABLE_PART
    public void ChangeWeapon(int index)
    {
        foreach(GameObject obj in character.weapons)
        {
            if(obj.GetComponent<Weapon>() != null)
            {
                if(obj.GetComponent<Weapon>().index == index)
                {
                    weapon = obj.GetComponent<Weapon>();
                    ActivateItem(obj);
                }
            }
        }
    }
    public void ChangePants(int index)
    {
        foreach (GameObject obj in character.hips)
        {
            if (obj.GetComponent<Pants>() != null)
            {
                if (obj.GetComponent<Pants>().index == index)
                {
                    pants = obj.GetComponent<Pants>();
                    ActivateItem(obj);
                }
            }
        }
    }
    public void ChangeLeftAccessary(int index)
    {
        foreach (GameObject obj in character.shoulder_Attachment_Left)
        {
            if (obj.GetComponent<Accessary>() != null)
            {
                if (obj.GetComponent<Accessary>().index == index)
                {
                    accessaryL = obj.GetComponent<Accessary>();
                    ActivateItem(obj);
                }
            }
        }
    }
    public void ChangeRightAccessary(int index)
    {
        foreach (GameObject obj in character.shoulder_Attachment_Right)
        {
            if (obj.GetComponent<Accessary>() != null)
            {
                if (obj.GetComponent<Accessary>().index == index)
                {
                    accessaryR = obj.GetComponent<Accessary>();
                    ActivateItem(obj);
                }
            }
        }
    }

    public void ChangeClothes(int index)
    {
        foreach (GameObject obj in character.torso)
        {
            if (obj.GetComponent<Clothes>() != null)
            {
                if (obj.GetComponent<Clothes>().index == index)
                {
                    clothes = obj.GetComponent<Clothes>();
                    ActivateItem(obj);
                }
            }
        }
    }
    public void ChangeHelmet(int index)
    {
        foreach (GameObject obj in character.headCoverings_Base_Hair)
        {
            if (obj.GetComponent<Helmet>() != null)
            {
                if (obj.GetComponent<Helmet>().index == index)
                {
                    helmet = obj.GetComponent<Helmet>();
                    ActivateItem(obj);
                }
            }
        }
    }
    public void ChangeBag(int index)
    {
        foreach(GameObject obj in character.back_Attachment)
        {
            if(obj.GetComponent<Bag>() != null)
            {
                if(obj.GetComponent<Bag>().index == index)
                {
                    bag = obj.GetComponent<Bag>();
                    ActivateItem(obj);
                }
            }
        }
    }
    #endregion VARIABLE_PART

    // method for handling the chance of left/right items to be differnt (such as shoulders, hands, legs, arms)
    public void ChangeLeftRight(List<GameObject> objectListRight, List<GameObject> objectListLeft, int index)
    {
        // rndPercent = chance for left item to be different

        // enable item from list using index
        ActivateItem(objectListRight[index]);
        // enable left item from list using index
        ActivateItem(objectListLeft[index]);
    }


    // enable game object and add it to the enabled objects list
    void ActivateItem(GameObject go)
    {
        // enable item
        go.SetActive(true);

        // add item to the enabled items list
        enabledObjects.Add(go);
    }

    // build all item lists for use in randomization
    private void BuildLists()
    {
        //build out character lists

        BuildList(character.headAllElements, "Male_Head_All_Elements");
        BuildList(character.headNoElements, "Male_Head_No_Elements");
        BuildList(character.eyebrow, "Male_01_Eyebrows");
        BuildList(character.facialHair, "Male_02_FacialHair");
        BuildList(character.torso, "Male_03_Torso");
        BuildList(character.arm_Upper_Right, "Male_04_Arm_Upper_Right");
        BuildList(character.arm_Upper_Left, "Male_05_Arm_Upper_Left");
        BuildList(character.arm_Lower_Right, "Male_06_Arm_Lower_Right");
        BuildList(character.arm_Lower_Left, "Male_07_Arm_Lower_Left");
        BuildList(character.hand_Right, "Male_08_Hand_Right");
        BuildList(character.hand_Left, "Male_09_Hand_Left");
        BuildList(character.hips, "Male_10_Hips");
        BuildList(character.leg_Right, "Male_11_Leg_Right");
        BuildList(character.leg_Left, "Male_12_Leg_Left");

        BuildList(character.all_Hair, "All_01_Hair");
        BuildList(character.all_Head_Attachment, "All_02_Head_Attachment");
        BuildList(character.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
        BuildList(character.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
        BuildList(character.headCoverings_No_Hair, "HeadCoverings_No_Hair");
        BuildList(character.chest_Attachment, "All_03_Chest_Attachment");
        BuildList(character.back_Attachment, "All_04_Back_Attachment");
        BuildList(character.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
        BuildList(character.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
        BuildList(character.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
        BuildList(character.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
        BuildList(character.hips_Attachment, "All_09_Hips_Attachment");
        BuildList(character.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
        BuildList(character.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
        BuildList(character.elf_Ear, "Elf_Ear");
        BuildList(character.weapons, "All_13_Weapon");
    }

    // called from the BuildLists method
    void BuildList(List<GameObject> targetList, string characterPart)
    {
        Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>();

        // declare target root transform
        Transform targetRoot = null;

        // find character parts parent object in the scene
        foreach (Transform t in rootTransform)
        {
            if (t.gameObject.name == characterPart)
            {
                targetRoot = t;
                break;
            }
        }

        // clears targeted list of all objects
        targetList.Clear();

        // cycle through all child objects of the parent object
        for (int i = 0; i < targetRoot.childCount; i++)
        {
            // get child gameobject index i
            GameObject go = targetRoot.GetChild(i).gameObject;

            // disable child object
            go.SetActive(false);

            // add object to the targeted object list
            targetList.Add(go);

        }
    }

    // classe for keeping the lists organized, allows for simple switching from character/female objects
    [System.Serializable]
    public class CharacterObjectGroups
    {
        public List<GameObject> headAllElements;
        public List<GameObject> headNoElements;
        public List<GameObject> eyebrow;
        public List<GameObject> facialHair;
        public List<GameObject> torso;
        public List<GameObject> arm_Upper_Right;
        public List<GameObject> arm_Upper_Left;
        public List<GameObject> arm_Lower_Right;
        public List<GameObject> arm_Lower_Left;
        public List<GameObject> hand_Right;
        public List<GameObject> hand_Left;
        public List<GameObject> hips;
        public List<GameObject> leg_Right;
        public List<GameObject> leg_Left;
        public List<GameObject> headCoverings_Base_Hair;
        public List<GameObject> headCoverings_No_FacialHair;
        public List<GameObject> headCoverings_No_Hair;
        public List<GameObject> all_Hair;
        public List<GameObject> all_Head_Attachment;
        public List<GameObject> chest_Attachment;
        public List<GameObject> back_Attachment;
        public List<GameObject> shoulder_Attachment_Right;
        public List<GameObject> shoulder_Attachment_Left;
        public List<GameObject> elbow_Attachment_Right;
        public List<GameObject> elbow_Attachment_Left;
        public List<GameObject> hips_Attachment;
        public List<GameObject> knee_Attachement_Right;
        public List<GameObject> knee_Attachement_Left;
        public List<GameObject> all_12_Extra;
        public List<GameObject> elf_Ear;
        public List<GameObject> weapons;
    }
}