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
        
        
        // TODO - Machine Manager that handles that
        
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
            
            currentProduct = new Product(new ProductData());
        }

        private void LoadCameras(GameObject camerasGo)
        {
            var cameras = Object.Instantiate(camerasGo).GetComponent<CameraComponents>();
            Release(camerasGo);

            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("JoystickCanvas", LoadJoystickCanvas);

            void LoadJoystickCanvas(GameObject joystickCanvasGo)
            {
                var joystickCanvas = Object.Instantiate(joystickCanvasGo).GetComponent<JoystickComponents>();
                Release(joystickCanvasGo);
                
                AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("SorcererController", LoadSorcererController);

                void LoadSorcererController(GameObject sorcererControllerGo)
                {
                    sorcererController = Object.Instantiate(sorcererControllerGo).GetComponent<SorcererController>();
                    Release(sorcererControllerGo);

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
            
            Debug.Log($"Interacting, product is {currentProduct?.name}");
            currentInteractable.Interact(currentProduct,out currentProduct);
            Debug.Log($"Interacted, product is now {currentProduct?.name}");
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