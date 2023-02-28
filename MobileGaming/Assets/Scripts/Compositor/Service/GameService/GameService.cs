using Addressables;
using Addressables.Components;
using Attributes;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private ISceneService sceneService;
        [DependsOnService] private IInputService inputService;

        private SorcererController sorcererController;
        private MachineManager machineManager;
        
        private Product currentProduct; // TODO - multiple products (product slot class list)
        
        private Interactable currentInteractable;

        [ServiceInit]
        public void InitGame()
        {
            LoadAssets();
        }

        private void LoadAssets()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Cameras", LoadCameras);
        }

        private void LoadGame()
        {
            machineManager.InitMachines();

            Interactable.ResetEvents();
            Interactable.OnRangeEnter += OnInteractableEnter;
            Interactable.OnRangeExit += OnInteractableExit;
        }

        private void LoadCameras(GameObject camerasGo)
        {
            var cameras = Object.Instantiate(camerasGo).GetComponent<CameraComponents>();
            Release(camerasGo);

            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("JoystickCanvas", LoadJoystickCanvas);

            void LoadJoystickCanvas(GameObject joystickCanvasGo)
            {
                var joystickCanvas = Object.Instantiate(joystickCanvasGo).GetComponent<JoystickComponents>();
                // TODO - apply sprites, then release
                //Release(joystickCanvasGo);
                
                AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("SorcererController", LoadSorcererController);

                void LoadSorcererController(GameObject sorcererControllerGo)
                {
                    sorcererController = Object.Instantiate(sorcererControllerGo).GetComponent<SorcererController>();
                    // TODO - apply materials, then release
                    //Release(sorcererControllerGo);

                    sorcererController.perspCameraGo = cameras.perspCameraGo;
                    sorcererController.perspCam = cameras.perspCamera;
                    sorcererController.orthoCam = cameras.othoCamera;

                    sorcererController.joystickParentGo = joystickCanvas.parentGo;
                    sorcererController.joystickParentTr = joystickCanvas.parentTr;
                    sorcererController.joystickTr = joystickCanvas.joystickTr;
                    
                    sorcererController.SetVariables();

                    sorcererController.OnInteract += InteractWithInteractable;

                    machineManager = Object.FindObjectOfType<MachineManager>(); // TODO - load it before, with the level layout
                    
                    LoadGame();
                }
            }
        }

        private void InteractWithInteractable()
        {
            if(currentInteractable is null) return;
            
            Debug.Log($"Interacting, product is {currentProduct}");
            currentInteractable.Interact(currentProduct,out currentProduct);
            Debug.Log($"Interacted, product is now {currentProduct}");
        }

        private void OnInteractableEnter(Interactable interactable)
        {
            currentInteractable = interactable;
        }
        
        private void OnInteractableExit(Interactable interactable)
        {
            if (currentInteractable == interactable) currentInteractable = null;
        }
    }
}